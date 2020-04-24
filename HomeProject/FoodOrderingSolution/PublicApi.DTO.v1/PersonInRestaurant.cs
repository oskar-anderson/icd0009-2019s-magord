using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PersonInRestaurant : PersonInRestaurantEdit
    {
        public ICollection<Restaurant> Restaurants { get; set; } = default!;
        public ICollection<Person> Persons { get; set; } = default!;
    }

    public class PersonInRestaurantDetail : PersonInRestaurantEdit
    {
        public Restaurant Restaurant { get; set; } = default!;
        public Person Person { get; set; } = default!;
    }
    
    public class PersonInRestaurantCreate
    {
        public string From { get; set; } = default!;
        public string To { get; set; } = default!;
        [MaxLength(256)] [MinLength(1)] public string Role { get; set; } = default!;
        public Guid PersonId { get; set; }
        public Guid RestaurantId { get; set; }
    }
    
    public class PersonInRestaurantEdit : PersonInRestaurantCreate
    {
        public Guid Id { get; set; }
    }
}