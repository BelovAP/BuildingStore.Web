using BuildingStore.Web.Models;
using FluentValidation;

namespace BuildingStore.Web.Validation;

public class MaterialValidator : AbstractValidator<BuildingMaterial>
{
    public MaterialValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Введите название");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Цена должна быть больше 0");
        RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Выберите категорию");
    }
}
