using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SMTP.WebUI.Models.Entities_Models
{
    public class ProjectTask
    {
        [Key]
        public int TaskId { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Введите название задачи")]
        [Index]
        public string TaskName { get; set; }

       
        [Required(ErrorMessage = "Введите исполнителя")]
        [Index]
        public int ExecuterId { get; set; }

        public string Discription { get; set; }

        [DataType(DataType.Date)]
        [Remote("ValidateDate", "Tasks")]
        [DisplayFormat(DataFormatString = "{0:yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public string Coments { get; set; }

        public double? PriceTask { get; set; }

        public int StateTask { get; set; }



        [Required(ErrorMessage = "Введите проект!")]
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
    }
}