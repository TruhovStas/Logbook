﻿namespace Logbook.BusinessLogic.Models.Equipments
{
    public class EquipmentUpdateDto : BaseResponseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
    }
}
