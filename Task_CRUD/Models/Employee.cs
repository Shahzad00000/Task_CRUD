using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace Task_CRUD.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Contact")]

        public string Contact { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Gender")]

        public Gen Gender { get; set; }
        [Required(ErrorMessage = "Please Enter Description")]

        public string Description { get; set; }
        [Required(ErrorMessage = "Please Enter Degidesignation")]

        public Degi Degidesignation { get; set; }
        [Required(ErrorMessage = "Please Enter Date")]

        public DateTime Date { get; set; }

        public string? Path { get; set; }
        [NotMapped]
        [Display(Name = "Choose Name")]
        public IFormFile FormFile { get; set; }
        public bool IsActive { get; set; }
    }
    public enum Degi
    {
        Software_Engineer, Electrical_Engineer, Mechenical
    }
    public enum Gen
    {
        Male, Female, Other
    }
}
