﻿using System;

namespace Pacco.Services.Availability.Application.DTO.External
{
    public class CustomerStateDto
    {
        public string State { get; set; }
        public bool IsValid => State.Equals("valid", StringComparison.OrdinalIgnoreCase);
    }
}
