﻿namespace StandingsForecaster.Domain.Model
{
    public class Team
    {
        public string Name { get; private set; }

        public Team (string name)
        {
            Name = name;
        }
    }
}