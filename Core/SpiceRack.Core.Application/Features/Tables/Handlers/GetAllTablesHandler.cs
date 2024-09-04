using System;
using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.TableResponses;
using SpiceRack.Core.Application.Features.Tables.Queries;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Tables.Handlers;

public class GetAllTablesHandler : IRequestHandler<GetAllTablesQuery, Response<IList<GetTableResponse>>>
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public GetAllTablesHandler(ITableRepository tableRepository, IMapper mapper)
    {
        _tableRepository = tableRepository;
        _mapper = mapper;
    }

    public async Task<Response<IList<GetTableResponse>>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
    {
        var tableList = await _tableRepository.GetAllAsync();
        var response = _mapper.Map<List<GetTableResponse>>(tableList);
        return new Response<IList<GetTableResponse>>(response);
    }
}
