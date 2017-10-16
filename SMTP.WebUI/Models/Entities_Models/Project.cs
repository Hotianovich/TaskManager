using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMTP.WebUI.Models.Entities_Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Введите название проекта")]
        [Index]
        [Display(Name = "Название проекта")]
        public string ProjectName { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<ProjectTask> ProjectTasks { get; set; }
        public Project()
        {
            ProjectTasks = new List<ProjectTask>();
        }


        
    }
}