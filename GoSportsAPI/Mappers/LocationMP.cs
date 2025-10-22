using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Mdels.Locations;

namespace GoSportsAPI.Mappers
{
    public static class LocationMP
    {
        public static LocationResponceDto ToLocationResponceDto(this Location locationModel)
        {
            return new LocationResponceDto
            {
                Id = locationModel.Id,
                Name = locationModel.Name,
                Description = locationModel.Description,
                LocationType = locationModel.LocationType,
                Lobbies = locationModel.Lobbies.Select(l => l.ToLobbyResponceDto()).ToList(),
                Sports = locationModel.Sports.Select(s => s.ToSportResponceDto()).ToList(),
                CurrentLobbyCount = locationModel.CurrentLobbyCount,
                MaxLobbyCount = locationModel.MaxLobbyCount
            };
        }

        public static Location ToLocationFromCreate(this LocationCreateDto locationDto)
        {
            return new Location
            {
                Name = locationDto.Name,
                Description = locationDto.Description,
                LocationType = locationDto.LocationType.ToLocationTypeFromCreate(),
                MaxLobbyCount = locationDto.MaxLobbyCount
            };
        }

        public static Location ToLocationFromUpdate(this LocationUpdateDto locationDto)
        {
            return new Location
            {
                Name = locationDto.Name,
                Description = locationDto.Description,
                LocationType = locationDto.LocationType.ToLocationTypeFromUpdate(),
                MaxLobbyCount = locationDto.MaxLobbyCount
            };
        }
    }
}





/* Find a fix for lobbies showing as null
"id": "c260c85f-ca27-4b67-a08e-6c47112e549d",
  "name": "Gym",
  "description": "A random gym that is nowhere",
  "locationType": {
    "id": "4f69a7d4-1e4b-4a0a-aa89-74340c94aae6",
    "locationId": "c260c85f-ca27-4b67-a08e-6c47112e549d",
    "name": "Gym",
    "isIndoor": true,
    "surface": "wood",
    "hasLights": false
  },
  "lobbies": [],
  "sports": null,
  "currentLobbyCount": 0,
  "maxLobbyCount": 5
}*/