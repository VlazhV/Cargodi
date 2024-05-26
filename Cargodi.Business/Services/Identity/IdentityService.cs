using System.Security.Cryptography;
using AutoMapper;
using Cargodi.Business.DTOs.Identity;
using Cargodi.Business.DTOs.Staff.Client;
using Cargodi.Business.DTOs.Staff.Driver;
using Cargodi.Business.DTOs.Staff.Operator;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Identity;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities;
using Cargodi.DataAccess.Entities.Staff;
using Cargodi.DataAccess.Interfaces.Staff;
using Microsoft.AspNetCore.Identity;

namespace Cargodi.Business.Services.Identity;

public class IdentityService : IIdentityService
{
    private readonly IUserRepository _userRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IOperatorRepository _operatorRepository;
    private readonly IDriverRepository _driverRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public IdentityService(
            IUserRepository userRepository,
            ITokenService tokenService,
            IClientRepository clientRepository,
            IDriverRepository driverRepository,
            IOperatorRepository operatorRepository,
            IMapper mapper)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;

        _clientRepository = clientRepository;
        _driverRepository = driverRepository;
        _operatorRepository = operatorRepository;
        
        _mapper = mapper;
    }
    
    public async Task<TokenDto> LoginAsync(LoginDto loginModel, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByNameAsync(loginModel.UserName!) 
            ?? throw new ApiException("User name and/or password are incorrect", ApiException.NotFound);
    
        var isPasswordValid = await _userRepository.CheckPasswordAsync(user, loginModel.Password!);
    
        if (!isPasswordValid)
        {
            throw new ApiException("User name and/or password are incorrect", ApiException.NotFound);
        }

        var accessToken = await _tokenService.GetTokenAsync(user);		

        return new TokenDto
        {
            AccessToken = accessToken,			
        };
    }

    public async Task<UserDto> RegisterAsync(RegisterDto registerDto, string? receptionistRole, CancellationToken cancellationToken)
    {
        await ValidateRegisterRequest(registerDto, cancellationToken);
        var user = _mapper.Map<User>(registerDto);
        
        if (await _userRepository.DoesItExistAsync(user, cancellationToken))
            throw new ApiException("email, phone number or user name is reserved", ApiException.BadRequest);

        var password = $"{user.UserName}_{registerDto.Role!.ToUpper()}_123";
        await HandleIdentityResultAsync(_userRepository.CreateAsync(user, password));
        
        await HandleIdentityResultAsync(_userRepository.AddToRoleAsync(user, registerDto.Role!));
        
        if (registerDto.Client != null)
        {
            await CreateRoleWithSaveAsync(registerDto.Client, Roles.Client, user.Id, cancellationToken);
        } 
        else if (registerDto.Driver != null)
        {
            await CreateRoleWithSaveAsync(registerDto.Driver, Roles.Driver, user.Id, cancellationToken);
        }
        else 
        {
            await CreateRoleWithSaveAsync(registerDto.Operator!, Roles.Manager, user.Id, cancellationToken);
        }
        
        var userDto = _mapper.Map<UserDto>(user);
        userDto.Role = (await _userRepository.GetRolesAsync(user)).First();

        await AssignRoleInfoAsync(userDto.Id, userDto, cancellationToken);
    
        return userDto;
    }


    public async Task<TokenDto> SignUpAsync(SignupDto signupDto, CancellationToken cancellationToken)
    {
        await ValidateSignupRequest(signupDto, cancellationToken);
        var user = _mapper.Map<User>(signupDto);

        if (await _userRepository.DoesItExistAsync(user, cancellationToken))
            throw new ApiException("email, phone number or user name is reserved", ApiException.BadRequest);
        
        await HandleIdentityResultAsync(_userRepository.CreateAsync(user, signupDto.Password!));			
        await _userRepository.AddToRoleAsync(user, Roles.Client);
        
        await CreateRoleWithSaveAsync(signupDto.Client!, Roles.Client, user.Id, cancellationToken);
       
        await _clientRepository.SaveChangesAsync(cancellationToken);
        var tokenDto = await LoginAsync(_mapper.Map<LoginDto>(signupDto), cancellationToken);

        return tokenDto;
    }

    private string GenerateRandom()
    {
        byte[] bytes = new byte[20];
        RandomNumberGenerator.Create().GetBytes(bytes);
        
        return Convert.ToBase64String(bytes);
    }
    
    private async Task HandleIdentityResultAsync(Task<IdentityResult> taskResult)
    {
        var result = await taskResult;
        if (!result.Succeeded)
        {			
            var message = result.Errors.Select(e => e.Description)
                .Aggregate((a, c) => $"{a}{c}");

            throw new ApiException(message, ApiException.BadRequest);
        }
    }

    private async Task AssignRoleInfoAsync(long id, UserDto userDto, CancellationToken cancellationToken)
    {        
        switch (userDto.Role)
        {
            case Roles.Manager:
                var @operator = await _operatorRepository.GetOperatorByUserIdAsync(id, cancellationToken);
                userDto.Operator = _mapper.Map<GetOperatorDto>(@operator);
                break;
                
            case Roles.Client:
                var client = await _clientRepository.GetClientByUserIdAsync(id, cancellationToken);
                userDto.Client = _mapper.Map<GetClientDto>(client);
                break;
                
            case Roles.Driver:
                var driver = await _driverRepository.GetDriverByUserIdAsync(id, cancellationToken);
                userDto.Driver = _mapper.Map<GetDriverDto>(driver);
                break;
                
            default:
                throw new ArgumentException();
        }
    }
    
    private async Task CreateRoleWithSaveAsync(object dto, string role, long userId, CancellationToken cancellationToken)
    {        
        switch (role)
        {
            case Roles.Manager:
            case Roles.Admin:
                var manager = _mapper.Map<Operator>(dto);
                manager.UserId = userId;
                manager.EmployDate = DateTime.UtcNow;
                manager.FireDate = null;
                await _operatorRepository.CreateAsync(manager, cancellationToken);
                break;
                
            case Roles.Client:
                var client = _mapper.Map<Client>(dto);
                client.UserId = userId;
                await _clientRepository.CreateAsync(client, cancellationToken);
                break;
                
            case Roles.Driver:
                var driver = _mapper.Map<Driver>(dto);
                driver.UserId = userId;
                driver.EmployDate = DateTime.UtcNow;
                driver.FireDate = null;
                driver.DriverStatusId = DriverStatuses.Works.Id;
                await _driverRepository.CreateAsync(driver, cancellationToken);
                break;
                
            default:
                throw new ArgumentException();
        }

        await _driverRepository.SaveChangesAsync(cancellationToken);
        
    }
    
    private async Task ValidateRegisterRequest(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        switch(registerDto.Role)
        {
            case Roles.Admin:
            case Roles.Manager:
                if (!(registerDto.Client == null && registerDto.Driver == null && registerDto.Operator != null))
                {
                    throw new ApiException("Invalid request", ApiException.BadRequest);
                }
                break;
                
            case Roles.Driver:
                if (!(registerDto.Client == null && registerDto.Driver != null && registerDto.Operator == null))
                {
                    throw new ApiException("Invalid request", ApiException.BadRequest);
                }
                break;
                
            case Roles.Client:
                if (!(registerDto.Client != null && registerDto.Driver == null && registerDto.Operator == null))
                {
                    throw new ApiException("Invalid request", ApiException.BadRequest);
                }
                break;
                
            default:
                throw new ApiException("Invalid request", ApiException.BadRequest);
        }
    }
    
    private async Task ValidateSignupRequest(SignupDto signUpDto, CancellationToken cancellationToken)
    {
        if (signUpDto.Client == null)
        {
            throw new ApiException("Invalid request", ApiException.BadRequest);
        }
    }
    
}