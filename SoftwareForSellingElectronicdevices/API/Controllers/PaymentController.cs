using Data.DTOs;
using Data.IRespositories;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IBaseRespository<PaymentDTO, PaymentDTO> respository) : BaseController<Payment, PaymentDTO, PaymentDTO>(respository)
    {

    }
}
