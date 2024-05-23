using System.Security.Cryptography;
using AutoMapper;
using Cargodi.Business.DTOs.Identity;
using Cargodi.Business.Exceptions;
using Cargodi.Business.Interfaces.Identity;
using Cargodi.DataAccess.Constants;
using Cargodi.DataAccess.Entities;
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
        var user = _mapper.Map<User>(registerDto);
        
        if (await _userRepository.DoesItExistAsync(user, cancellationToken))
            throw new ApiException("email, phone number or user name is reserved", ApiException.BadRequest);

        var password = $"{user.UserName}_{registerDto.Role!.ToUpper()}_123";
        await HandleIdentityResultAsync(_userRepository.CreateAsync(user, password));
        
        await HandleIdentityResultAsync(_userRepository.AddToRoleAsync(user, registerDto.Role!));
        
        
        
        var userDto = _mapper.Map<UserDto>(user);
        userDto.Role = (await _userRepository.GetRolesAsync(user)).First();
    
        return userDto;
    }

    public async Task<TokenDto> SignUpAsync(SignupDto signupDto, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(signupDto);

        if (await _userRepository.DoesItExistAsync(user, cancellationToken))
            throw new ApiException("email, phone number or user name is reserved", ApiException.BadRequest);
        
        await HandleIdentityResultAsync(_userRepository.CreateAsync(user, signupDto.Password!));			
        await _userRepository.AddToRoleAsync(user, Roles.Client);
        
        
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

    private async Task CreateOperatorAsync()
    {
        
    }
    
    private async Task CreateDriverAsync()
    {
        
    }
    
    private async Task CreateClient()
    {
        
    }
}