using System;
using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using SpiceRack.Core.Application.DTOs.Requests.TableRequests;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Features.Tables.Commands;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;
using SpiceRack.Core.Domain.Entities;

namespace SpiceRack.Core.Application.Features.Tables.Handlers;

public class ChangeTableStatusHandler : IRequestHandler<ChangeTableStatusCommand, Response<Unit>>
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public ChangeTableStatusHandler(ITableRepository tableRepository, IMapper mapper)
    {
        _tableRepository = tableRepository;
        _mapper = mapper;
    }

    public async Task<Response<Unit>> Handle(ChangeTableStatusCommand request, CancellationToken cancellationToken)
    {
      var table = await _tableRepository.GetByIdAsync(request.Id);
      if (table == null) throw new ApiException("table not found",(int)HttpStatusCode.NotFound);

      request.StatusRequest.ApplyTo(table);
      await _tableRepository.UpdateAsync(table, request.Id);

      return new Response<Unit>(Unit.Value);
    }
}
