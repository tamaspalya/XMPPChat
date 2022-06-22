using System.Collections.Generic;
using DataAccess.Models;

namespace ChatserviceAPI.DTOs.Converters
{
    public static class UserDTOConverter
    {
        public static UserDTO ToDTO(this User userToConvert)
        {
            var userDTO = new UserDTO();
            userToConvert.CopyPropertiesTo(userDTO);
            return userDTO;
        }
        public static User FromDTO(this UserDTO DTOToConvert)
        {
            var user= new User();
            DTOToConvert.CopyPropertiesTo(user);
            return user;
        }
        public static IEnumerable<UserDTO> ToDtos(this IEnumerable<User> usersToConvert)
        {
            foreach (var user in usersToConvert)
            {
                yield return user.ToDTO();
            }
        }

        public static IEnumerable<User> FromDtos(this IEnumerable<UserDTO> userDtosToConvert)
        {
            foreach (var userDto in userDtosToConvert)
            {
                yield return userDto.FromDTO();
            }
        }
    }
}
