using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
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
    [Route("")]
    public class BattleshipStateApiController : ControllerBase
    {
        private readonly ILogger<BattleshipStateApiController> _logger;
        private readonly IBoardCoordinator _boardCoordinator;
        
        public BattleshipStateApiController(ILogger<BattleshipStateApiController> logger, IBoardCoordinator boardCoordinator)
        {
            _logger = logger;
            _boardCoordinator = boardCoordinator;
        }

        [HttpGet]
        [Route("")]
        public string GetBlank()
        {
            return "Application has been hosted please talk to developer for route information.";
        }

        [HttpGet]
        [Route("Create")]
        public IBoard Get()
        {
            try
            {
                var localBoard = _boardCoordinator.CreateBoard();

                HttpContext.Session.Set("LocalB", BinaryBoardSerialiazer.SerializeObject(localBoard));
                return localBoard;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Message),
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpPost]
        [Route("AddBattleShip")]
        public AddBattleshipResponse AddBattleShip([FromBody] AddBattleshipRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("The request object is null.");
            }
            try
            {
                var orientation = (OrientationType)Enum.Parse(typeof(OrientationType), request.Orientation);

                byte[] deserial;
                bool canRead = HttpContext.Session.TryGetValue("LocalB", out deserial);
                if (canRead)
                {
                    var localB = BinaryBoardSerialiazer.DeSerializeObject(deserial);
                    bool result = _boardCoordinator.AddBattleship(localB, new BoardCell()
                    {
                        XCoordinate = request.XCoordinate,
                        YCoordinate = request.YCoordinate
                    }, new Battleship() { Orientation = orientation, Width = request.Width });

                    HttpContext.Session.Set("LocalB", BinaryBoardSerialiazer.SerializeObject(localB));
                    return new AddBattleshipResponse() { Result = result, Board = localB };
                }
                else
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("The game board has not been created. Please create a game board before attacking."),
                        ReasonPhrase = "Board Not Found."
                    };
                    throw new System.Web.Http.HttpResponseException(resp);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Message),
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpPost]
        [Route("Attack")]
        public AttackResponse Attack([FromBody] AttackRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("The request object is null.");
            }
            
            try
            {
                byte[] deserial;
                bool canRead = HttpContext.Session.TryGetValue("LocalB", out deserial);
                if (canRead)
                {
                    var localB = BinaryBoardSerialiazer.DeSerializeObject(deserial);
                    var HitOrMiss = _boardCoordinator.Attack(localB, new BoardCell()
                    {
                        XCoordinate = request.XCoordinate,
                        YCoordinate = request.YCoordinate
                    });
                    HttpContext.Session.Set("LocalB", BinaryBoardSerialiazer.SerializeObject(localB));
                    return new AttackResponse() { Board = localB, Result = Enum.GetName(typeof(AttackResult), HitOrMiss) };
                }
                else
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("The game board has not been created. Please create a game board before attacking."),
                        ReasonPhrase = "Board Not Found"
                    };
                    throw new System.Web.Http.HttpResponseException(resp);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Message),
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
            
        }
    }
}
