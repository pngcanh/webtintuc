using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace webtintuc.Controllers;
public class AdminCPController : Controller
{

    [Authorize(Roles = "admin")]
    // [Route("AdminControll")]
    public IActionResult Index() => View();
}
