using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RulesExercise.Domain.Entities;
using RulesExercise.Domain.Enums;

namespace RulesExercise.Infrastructure.Persistence.Configurations
{
    public class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.Property(it => it.Type)
                .IsRequired();
            
            builder.Property(it => it.Message)
                .IsRequired()
                .HasMaxLength(1000);
            
            builder.Property(it => it.Subject)
                .IsRequired();

            builder.HasData(
                    new Template()
                    {
                        Id = 1,
                        Type = nameof(Channel.Smtp),
                        Message = @"
<div>
	<h4>Hi there from {{ Name }} project</h4>
	<ul>
		<li>Id : {{ Id }};</li>
        <li>Project Changed;</li>
	</ul> 
</div>",
                        Subject = @"Changes from {{Name}} project"
                    },
                    new Template()
                    {
                        Id = 2,
                        Type = nameof(Channel.Smtp),
                        Message = @"
<div>
	<h4>Project {{ Name }} was Deleted</h4>
	<ul>
		<li>Id : {{ Id }};</li>
        <li>Project Deleted;</li>
	</ul> 
</div>",
                        Subject = @"{{ Name }} project was deleted"
                    },
                    new Template()
                    {
                        Id = 3,
                        Type = nameof(Channel.Telegram),
                        Subject = "{{ Name }} project with id  {{ Id }} was changed",
                        Message = "Description: {{ Description }}"
                    },
                    new Template()
                    {
                        Id = 4,
                        Type = nameof(Channel.Telegram),
                        Subject = "{{ Name }} project was deleted",
                        Message = "Description: {{ Description }}\n Id: {{ Id }}"
                    }
                );
        }
    }
}
