using GoSportsAPI.Dtos.Sports;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Mappers
{
    /// <summary>Mapping profiles for sports.</summary>
    public static class SportMP
    {
        /// <summary>
        ///   <para>
        /// Converts to SportResponceDto from a sport model</para>
        /// </summary>
        /// <param name="sportModel">The sport model.</param>
        /// <returns>SportResponceDto</returns>
        public static SportResponceDto ToSportResponceDto(this Sport sportModel)
        {
            return new SportResponceDto
            {
                SportId = sportModel.SportId,
                Name = sportModel.Name
            };
        }


        /// <summary>Converts to a sport from a create Dto.</summary>
        /// <param name="sportDto">The sport create dto.</param>
        /// <returns>Sport</returns>
        public static Sport ToSportFromCreate(this SportCreateDto sportDto)
        {
            return new Sport
            {
                Name = sportDto.Name,
            };
        }


        /// <summary>Converts to a sport from an update Dto.</summary>
        /// <param name="sportDto">The sport update dto.</param>
        /// <returns>Sport</returns>
        public static Sport ToSportFromUpdate(this SportUpdateDto sportDto)
        {
            return new Sport
            {
                Name = sportDto.Name,
            };
        }
    }
}
