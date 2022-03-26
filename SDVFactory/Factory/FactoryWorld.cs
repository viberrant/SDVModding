﻿using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Dimensions;

namespace SDVFactory.Factory
{
    public class FactoryWorld
    {
        public long NextMachineId { get; set; } = -1;
        public Dictionary<long, Machines.Machine> Machines { get; set; } = new Dictionary<long, Machines.Machine>();

        public void ActivateMachine(GameLocation l, Farmer who, Furniture f, Location vect, long machine)
        {
            if (Machines.ContainsKey(machine))
            {
                Machines[machine].OnActivate(l, who, f, vect);
            }
        }
    }
}