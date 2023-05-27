using Application.eServicesPortal.DTOs;
using Application.eServicesPortal.Interfaces;
using Application.eServicesPortal.Users.Query;
using AutoMapper;


namespace Application.eServicesPortal.ApplicationServices
{
   
    public class AppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AppService(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository= userRepository;
            _mapper= mapper;
        }

        public async Task<UserDTO> GetUser(GetUserQuery query)
        {
            var user= await _userRepository.GetUser(query.UserId);
            UserDTO userDTO=_mapper.Map<UserDTO>(user); 
            return userDTO;
        }
        public async Task<List<UserDTO>> GetUsers()
        {

            var userlist = await _userRepository.GetUsers();
            List<UserDTO> userDTOList = _mapper.Map<List<UserDTO>>(userlist);
            return userDTOList;
            
        }
    }
}
