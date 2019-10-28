using BattleShip.BattleshipApiEntities;
using IL = InterfaceLibrary;
using Microsoft.Extensions.DependencyInjection;
using System;
using BoardGameCoordinator;
using BattleshipPlacementValidator;
using ShipAttacker;

namespace DependencyResolver
{
    public static class DependencyResolver
    {
        public static void ResolveDependencies(IServiceCollection services)
        {
            services.AddTransient(typeof(IL.IBoardCoordinator), typeof(BattleshipCoordinator));
            services.AddTransient(typeof(IL.IShipPlacementValidator), typeof(BattleshipValidator));
            services.AddTransient(typeof(IL.IShipAttacker), typeof(BattleshipAttacker));
        }
    }
}
