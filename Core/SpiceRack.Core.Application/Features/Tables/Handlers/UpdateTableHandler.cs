
using System.Net;
using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.TableResponses;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Features.Tables.Commands;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;
using SpiceRack.Core.Domain.Entities;

namespace SpiceRack.Core.Application.Features.Tables.Handlers;

public class UpdateTableHandler : IRequestHandler<UpdateTableCommand, Response<GetTableResponse>>
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public UpdateTableHandler(ITableRepository tableRepository, IMapper mapper)
    {
        _tableRepository = tableRepository;
        _mapper = mapper;
    }

    public async Task<Response<GetTableResponse>> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        var table = await _tableRepository.GetByIdAsync(request.TableRequest.Id);
        if (table == null) throw new ApiException("table not found",(int)HttpStatusCode.NotFound);

        var result = _mapper.Map<Table>(request.TableRequest);
        await _tableRepository.UpdateAsync(result, table.Id);

        var response = _mapper.Map<GetTableResponse>(result);
        return new Response<GetTableResponse>(response);
  
    }
}
