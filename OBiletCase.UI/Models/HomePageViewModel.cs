﻿using OBiletCase.Model.JourneyOffer;

namespace OBiletCase.UI.Models
{
    public class HomePageViewModel
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public SelectBoxModel? LastOrigin { get; set; }
        public SelectBoxModel? LastDestination { get; set; }
    }
}
