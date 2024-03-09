// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Rooms.Commands.AddEdit;

public class AddEditRoomCommandValidator : AbstractValidator<AddEditRoomCommand>
{
    public AddEditRoomCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(256)
            .NotEmpty();
        RuleFor(v => v.RoomImages).NotEmpty().WithMessage("Please upload product pictures.");
        RuleFor(v => v.UploadPictures).NotEmpty().When(x => x.RoomImages == null || !x.RoomImages.Any())
            .WithMessage("Please upload product pictures.");
    }
}