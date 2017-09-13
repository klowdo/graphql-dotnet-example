using System;
using System.Collections.Generic;

namespace graphql_dotnet.Models.Dto
{
    public class Customer: Entity<Guid>
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Note> Notes { get; set; } = new HashSet<Note>();
        public ICollection<Engagement> Engagements { get; set; } = new HashSet<Engagement>();
        public Note AddNote(string text, string createdBy){
            var newNote = new Note{
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                CreatedBy= createdBy,
                Text = text
            };
            Notes.Add(newNote);
            return newNote;
        }

        public void AddEngagement(Engagement engagement) => Engagements.Add(engagement);
    }
   
}