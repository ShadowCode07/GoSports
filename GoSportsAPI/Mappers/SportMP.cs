using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Mappers
{
    public static class SportMP
    {
        public static SportResponceDto ToSportResponceDto(this Sport sportModel)
        {
            return new SportResponceDto
            {
                Id = sportModel.Id,
                Name = sportModel.Name,
            };
        }

        public static Sport ToSportFromCreate(this SportCreateDto sportDto)
        {
            return new Sport
            {
                Name = sportDto.Name,
            };
        }

        public static Sport ToSportFromUpdate(this SportUpdateDto sportDto)
        {
            return new Sport
            {
                Name = sportDto.Name,
            };
        }
    }
}
