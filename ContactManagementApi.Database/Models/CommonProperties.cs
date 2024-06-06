using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManagementApi.Database.Models
{
    public class CommonProperties
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto ID duombazeje
        public Guid Id { get; set; }
    }
}