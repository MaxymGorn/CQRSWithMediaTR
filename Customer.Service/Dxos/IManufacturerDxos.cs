using Customer.Domain.Dtos;
using Customer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Service.Dxos
{
    public interface IManufacturerDxos
    {
        List<ManufacturerDto> MapListManufacturerDto(IEnumerable<Manufacturer> manufacturers);
        ManufacturerDto MapManufacturerDto(Manufacturer manufacturer);
    }
}
