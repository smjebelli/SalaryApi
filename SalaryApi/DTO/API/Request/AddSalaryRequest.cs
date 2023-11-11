using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace SalaryApi.DTO.API.Request
{
    public class AddSalaryRequest
    {
        public IFormFile Data { get; set; }
        
        [Required]
        public string OverTimeCaluculator { get; set; }
    }
}
