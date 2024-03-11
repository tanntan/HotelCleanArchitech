// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.RoomTypes.Commands.AddEdit;

public class AddEditRoomTypeCommandValidator : AbstractValidator<AddEditRoomTypeCommand>
{
    public AddEditRoomTypeCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(256)
            .NotEmpty();
        RuleFor(v=> v.Description)
            .MinimumLength(10)
            .NotEmpty()
            .WithMessage("Please Input description more then 10 charactor");
        RuleFor(v => v.Status).NotEmpty().WithMessage("Please Input Status pictures.");
    }
}