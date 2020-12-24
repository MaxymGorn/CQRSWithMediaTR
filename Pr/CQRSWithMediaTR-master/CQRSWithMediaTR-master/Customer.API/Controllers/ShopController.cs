using Customer.Domain.Commands;
using Customer.Domain.Other;
using Customer.Domain.Queries;
using Customer.Domain.TypeofApiResponce;
using Customer.Domain.TypeofApiResponce.CategoryGetList;
using Customer.Domain.TypeofApiResponce.GetCategory;
using Customer.Domain.TypeofApiResponce.GetGoodList;
using Customer.Domain.TypeofApiResponce.GetManufacturer;
using Customer.Domain.TypeofApiResponce.GetManufacturerList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Queries;
using System;
using System.Threading.Tasks;
using ContentResult = Microsoft.AspNetCore.Mvc.ContentResult;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Customer.API.Controllers
{
    [Route("api/shop")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ShopController : ApiControllerBase

    {
        public ShopController(IMediator mediator) : base(mediator)
        {
        }
        #region Goods
        /// <summary>
        /// Create Goods
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("CreateGoods")]
        [ProducesResponseType(500, Type = typeof(GetServerErrorResponce))]
        [ProducesResponseType(200, Type = typeof(GetOkResponce))]
        public async Task<ActionResult<bool>> CreateGoods(CreateGoodsCommand command)
        {
            InternalServerErrorObjectResult internalServerErrorObjectResult = new InternalServerErrorObjectResult();
            try
            {
                await CommandAsync(command);
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status200OK;
                internalServerErrorObjectResult.Message = $"Good saved successfully!";
            }
            catch (Exception exception)
            {
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status500InternalServerError;
                internalServerErrorObjectResult.Message = $"Good not saved!!";
                internalServerErrorObjectResult.Desciption = exception.Message;
            }
            return CreateResult(internalServerErrorObjectResult);
        }
        /// <summary>
        /// Create Goods in background
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("CreateGoodsBackGround")]
        [ProducesResponseType(500, Type = typeof(GetServerErrorResponce))]
        [ProducesResponseType(200, Type = typeof(GetOkResponce))]
        public async Task<ActionResult<bool>> CreateGoodsBackGround(CreateGoodsCommandBackGround command)
        {
            InternalServerErrorObjectResult internalServerErrorObjectResult = new InternalServerErrorObjectResult();
            try
            {
                CommandAsyncBackGround(command);
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status200OK;
                internalServerErrorObjectResult.Message = $"Good start BackGround saved successfully!";
            }
            catch (Exception exception)
            {
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status500InternalServerError;
                internalServerErrorObjectResult.Message = $"Good in BackGround not saved!";
                internalServerErrorObjectResult.Desciption = exception.Message;
            }
            return CreateResult(internalServerErrorObjectResult);
        }
        /// <summary>
        /// Get List Goods
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet("GetListGoods")]
        [ProducesResponseType(500, Type = typeof(GetServerErrorResponce))]
        [ProducesResponseType(typeof(GetListGoods), 200)]
        public async Task<dynamic> GetListGoodsQuery([FromQuery] GetListGoodsQuery query)
        {
            InternalServerErrorObjectResult internalServerErrorObjectResult = new InternalServerErrorObjectResult();
            try
            {
                dynamic result = Single(await QueryAsync(query)).Result;
                internalServerErrorObjectResult.Message = result.Value;
            }
            catch (Exception exception)
            {
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status500InternalServerError;
                internalServerErrorObjectResult.Message = $"Error!";
                internalServerErrorObjectResult.Desciption = exception.Message;
            }
            return CreateResult(internalServerErrorObjectResult);
        }
        #endregion
        #region Category
        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("CreateCategory")]
        [ProducesResponseType(500, Type = typeof(GetServerErrorResponce))]
        [ProducesResponseType(200, Type = typeof(GetOkResponce))]
        public async Task<ContentResult> CreateCategory(CreateCategoryCommand command)
        {
            InternalServerErrorObjectResult internalServerErrorObjectResult = new InternalServerErrorObjectResult();
            try
            {
                await CommandAsync(command);
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status200OK;
                internalServerErrorObjectResult.Message = $"Category saved successfully!";
            }
            catch (Exception exception)
            {
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status500InternalServerError;
                internalServerErrorObjectResult.Message = $"Category not saved!!";
                internalServerErrorObjectResult.Desciption = exception.Message;
            }
            return CreateResult(internalServerErrorObjectResult);
        }
        /// <summary>
        /// Create Category in background
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("CreateCategoryBackGround")]
        [ProducesResponseType(500, Type = typeof(GetServerErrorResponce))]
        [ProducesResponseType(200, Type = typeof(GetOkResponce))]
        public async Task<ContentResult> CreateCategoryBackGround(CreateCategoryCommandBackGround command)
        {
            InternalServerErrorObjectResult internalServerErrorObjectResult = new InternalServerErrorObjectResult();
            try
            {
                CommandAsyncBackGround(command);
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status200OK;
                internalServerErrorObjectResult.Message = $"Category start BackGround saved successfully!";
            }
            catch (Exception exception)
            {
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status500InternalServerError;
                internalServerErrorObjectResult.Message = $"Category in BackGround not saved!";
                internalServerErrorObjectResult.Desciption = exception.Message;
            }
            return CreateResult(internalServerErrorObjectResult);
        }
        /// <summary>
        /// Get List Category
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet("GetListCategory")]
        [ProducesResponseType(500, Type = typeof(GetServerErrorResponce))]
        [ProducesResponseType(typeof(ResponceListCategory), 200)]
        public async Task<dynamic> GetListCategoryQuery([FromQuery] GetListCategoryQuery query)
        {
            InternalServerErrorObjectResult internalServerErrorObjectResult = new InternalServerErrorObjectResult();
            try
            {
                dynamic result= Single(await QueryAsync(query)).Result;
                internalServerErrorObjectResult.Message = result.Value;
            }
            catch(Exception exception)
            {
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status500InternalServerError;
                internalServerErrorObjectResult.Message = $"Error!";
                internalServerErrorObjectResult.Desciption = exception.Message;
            }
            return CreateResult(internalServerErrorObjectResult);
        }
        /// <summary>
        /// Get Categories by id
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet("GetCategory")]
        [ProducesResponseType(typeof(GetServerErrorResponce), 500)]
        [ProducesResponseType(typeof(GetCategory), 200)]
        public async Task<dynamic> ContainCategoryQuery([FromQuery] GetCategoryQuery query)
        {
            InternalServerErrorObjectResult internalServerErrorObjectResult = new InternalServerErrorObjectResult();
            try
            {
                dynamic result = Single(await QueryAsync(query)).Result;
                internalServerErrorObjectResult.Message = result.Value;
            }
            catch (Exception exception)
            {
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status500InternalServerError;
                internalServerErrorObjectResult.Message = $"Category does not exist!";
                internalServerErrorObjectResult.Desciption = exception.Message;
            }
            return CreateResult(internalServerErrorObjectResult);
        }
        #endregion
        #region Manufacturer
        /// <summary>
        /// Get List Manufacturer
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet("GetListManufacturer")]
        [ProducesResponseType(500, Type = typeof(GetServerErrorResponce))]
        [ProducesResponseType(typeof(GetManufacturerList), 200)]
        public async Task<dynamic> GetListManufacturerQuery([FromQuery] GetListManufacturerQuery query)
        {
            InternalServerErrorObjectResult internalServerErrorObjectResult = new InternalServerErrorObjectResult();
            try
            {
                dynamic result = Single(await QueryAsync(query)).Result;
                internalServerErrorObjectResult.Message = result.Value;
            }
            catch (Exception exception)
            {
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status500InternalServerError;
                internalServerErrorObjectResult.Message = $"Error!";
                internalServerErrorObjectResult.Desciption = exception.Message;
            }
            return CreateResult(internalServerErrorObjectResult);
        }
        /// <summary>
        /// Create Manufacturer
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("CreateManufacturer")]
        [ProducesResponseType(500, Type = typeof(GetServerErrorResponce))]
        [ProducesResponseType(200, Type = typeof(GetOkResponce))]
        public async Task<ContentResult> CreateManufacturer(CreateManufacturerCommand command)
        {
            InternalServerErrorObjectResult internalServerErrorObjectResult = new InternalServerErrorObjectResult();
            try
            {
                await CommandAsync(command);
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status200OK;
                internalServerErrorObjectResult.Message = $"Manufacturer saved successfully!";
            }
            catch (Exception exception)
            {
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status500InternalServerError;
                internalServerErrorObjectResult.Message = $"Manufacturer not saved!!";
                internalServerErrorObjectResult.Desciption = exception.Message;
            }
            return CreateResult(internalServerErrorObjectResult);
        }
        /// <summary>
        /// Create Manufacturer in background
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("CreateManufacturerBackGround")]
        [ProducesResponseType(500, Type = typeof(GetServerErrorResponce))]
        [ProducesResponseType(200, Type = typeof(GetOkResponce))]
        public async Task<ContentResult> CreateManufacturerBackGround(CreateManufacturerCommandBackGround command)
        {
            InternalServerErrorObjectResult internalServerErrorObjectResult = new InternalServerErrorObjectResult();
            try
            {
                CommandAsyncBackGround(command);
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status200OK;
                internalServerErrorObjectResult.Message = $"Manufacturer start BackGround saved successfully!";
            }
            catch (Exception exception)
            {
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status500InternalServerError;
                internalServerErrorObjectResult.Message = $"Manufacturer in BackGround not saved!";
                internalServerErrorObjectResult.Desciption = exception.Message;
            }
            return CreateResult(internalServerErrorObjectResult);
        }

        /// <summary>
        /// Get Manufacturers by id
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet("GetManufacturer")]
        [ProducesResponseType(typeof(GetServerErrorResponce), 500)]
        [ProducesResponseType(typeof(GetManufacturer), 200)]
        public async Task<dynamic> ContainManufacturerQuery([FromQuery] GetManufacturerQuery query)
        {
            InternalServerErrorObjectResult internalServerErrorObjectResult = new InternalServerErrorObjectResult();
            try
            {
                dynamic result = Single(await QueryAsync(query)).Result;
                internalServerErrorObjectResult.Message = result.Value;
            }
            catch (Exception exception)
            {
                internalServerErrorObjectResult.StatusCode = StatusCodes.Status500InternalServerError;
                internalServerErrorObjectResult.Message = $"Manufacturer does not exist!";
                internalServerErrorObjectResult.Desciption = exception.Message;
            }
            return CreateResult(internalServerErrorObjectResult);
        }
        #endregion
    }
}
