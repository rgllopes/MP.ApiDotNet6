using MP.ApiDotNet6.Application.DTOs.User;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interface;
using MP.ApiDotNet6.Domain.Authentication;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;

        public UserService(IUserRepository userRepository, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResultService<dynamic>> GenerateTokenAsync(UserDTO userDTO)
        {
            //Verifica se a DTO possui informações
            if (userDTO == null)
                return ResultService.Fail<dynamic>("Objeto deve ser informado!");

            //Verifica se os dados passados são válidos
            var validator = new UserDTOValidator().Validate(userDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<dynamic>("Problemas com a validação!", validator);

            //Verifica se o susário e senha existem no banco
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(userDTO.Email, userDTO.Password);
            if (user == null)
                return ResultService.Fail<dynamic>("Usuario ou senha não encontrado!");


            //Envia token de autenticação
            return ResultService.Ok(_tokenGenerator.Generator(user));
        }
    }
}
