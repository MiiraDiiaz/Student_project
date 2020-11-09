using AdmissionsCommittee.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionsCommittee.ViewModels
{
    public class ProfileViewModel
    {
        public string UserId { get; set; }

        public string Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required]
        [Display(Name = "Пол")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "День рождения")]
        public string BirthDay { get; set; }

        public string UserDocumetsId { get; set; }
        [Required]
        [Display(Name = "Выберите тип документа")]
        public string TypeDocument { get; set; }


        [Required]
        [Display(Name = "Введите серию и номер документа")]
        public string SeriesNamberDocument { get; set; }

        [Required]
        [Display(Name = "Дата Выдачи документа")]
        public string DateIssuanceDicument { get; set; }

        [Required]
        [Display(Name = "Кем выдан документ")]
        public string IssuanceOffice { get; set; }

        [Required]
        [Display(Name = "Место рождения")]
        public string BirthPlace { get; set; }

        [Required]
        [RegularExpression(@"^\+[1-9]\d{3}-\d{3}-\d{4}$", ErrorMessage = "Номер телефона должен иметь формат +xxxx-xxx-xxxx")]
        [Display(Name = "Введите номер телефона")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Загрузите скан документа")]
        public IFormFile IdentityDocument { get; set; }

        [Required]
        [Display(Name = "Загрузите фотографию")]
        public IFormFile Photo { get; set; }
    }
}
