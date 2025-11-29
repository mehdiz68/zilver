using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zilver.data;

namespace zilver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "JwtBearer")] // فقط توکن JWT
    public class ProductsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ProductsApiController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _db.Products.ToListAsync());
    }

}
