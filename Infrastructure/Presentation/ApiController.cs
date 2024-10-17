global using Microsoft.AspNetCore.Mvc;
global using Shared.ErrorModels;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    // lw hatet ba3d "api/[controller]" 3amlt /Action kda esm el action aw el endpoint hayzhar fel Route
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
    public class ApiController:ControllerBase
    {
    }
}
