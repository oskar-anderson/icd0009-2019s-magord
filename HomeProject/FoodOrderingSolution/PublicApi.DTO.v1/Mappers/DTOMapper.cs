using System;

namespace PublicApi.DTO.v1.Mappers
{
    public class DTOMapper
    {
        
        public Town MapTown(BLL.App.DTO.Town BLLTown)
        {
            return new Town()
            {
                Id = BLLTown.Id,
                Name = BLLTown.Name,
                AreaCount = BLLTown.AreaCount
            };
        }
    }
}