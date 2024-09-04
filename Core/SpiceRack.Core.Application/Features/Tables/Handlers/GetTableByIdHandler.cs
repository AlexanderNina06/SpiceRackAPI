using System;
using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.TableResponses;
using SpiceRack.Core.Application.Features.Tables.Queries;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Tables.Handlers;

public class GetTableByIdHandler : IRequestHandler<GetTableByIdQuery, Response<GetTableResponse>>
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public GetTableByIdHandler(ITableRepository tableRepository, IMapper mapper)
    {
        _tableRepository = tableRepository;
        _mapper = mapper;
    }

    public async Task<Response<GetTableResponse>> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
    {
        var table = await _tableRepository.GetByIdAsync(request.Id);
        
        if(table == null)
          return null;
        
        var response = _mapper.Map<GetTableResponse>(table);
        return new Response<GetTableResponse>(response);
    }
}
