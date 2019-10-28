using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BattleShip.BattleshipApiEntities;
using BattleshipStateApi.Models;
using Enumerations;
using InterfaceLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BattleshipStateApi.Controllers
{
    [ApiController]
    [Route("Battleship")]
    public class BattleshipStateApiController : ControllerBase
    {
        private readonly ILogger<BattleshipStateApiController> _logger;
        private readonly IBoardCoordinator _boardCoordinator;
        private static IBoard localBoard;

        public BattleshipStateApiController(ILogger<BattleshipStateApiController> logger, IBoardCoordinator boardCoordinator)
        {
            _logger = logger;
            _boardCoordinator = boardCoordinator;
        }

        [HttpGet]
        [Route("Create")]
        public IBoard Get()
        {
            localBoard = _boardCoordinator.CreateBoard();
            return localBoard;
        }

        [HttpPost]
        [Route("AddBattleShip")]
        public AddBattleshipResponse AddBattleShip([FromBody] AddBattleshipRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("The request object is null.");
            }
            var orientation = (OrientationType)Enum.Parse(typeof(OrientationType), request.Orientation);
            bool result = _boardCoordinator.AddBattleship(localBoard, new BoardCell()
            {
                XCoordinate = request.XCoordinate,
                YCoordinate = request.YCoordinate
            }, new Battleship() { Orientation = orientation, Width = request.Width });
            return new AddBattleshipResponse() { Result = result, Board = localBoard };
        }

        [HttpPost]
        [Route("Attack")]
        public AttackResponse Attack([FromBody] AttackRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("The request object is null.");
            }
            if (localBoard == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("The game board has not been created. Please create a game board before attacking."),
                    ReasonPhrase = "Product ID Not Found"
                };
                throw new System.Web.Http.HttpResponseException(resp);
            }
            try
            {
                var HitOrMiss = _boardCoordinator.Attack(localBoard, new BoardCell()
                {
                    XCoordinate = request.XCoordinate,
                    YCoordinate = request.YCoordinate
                });
                return new AttackResponse() { Board = localBoard, Result = Enum.GetName(typeof(AttackResult),HitOrMiss) };
            }
            catch(Exception ex)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.InternalServerError);
            }
            
        }
    }
}
