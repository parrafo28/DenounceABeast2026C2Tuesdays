using AutoMapper;
using DenounceBeasts.Application.Models.Dtos;
using DenounceBeasts.Application.Models.Responses;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infraestructure.Repository;

namespace DenounceBeasts.Application.Services
{
    public class MunicipalityServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MunicipalityServices(UnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public ApiResponse<List<MunicipalityDto>> GetAllMunicipalities()
        {
            var _municipalities = _unitOfWork.Municipality.GetAll();
            var response = _mapper.Map<List<MunicipalityDto>>(_municipalities);
            return ApiResponse<List<MunicipalityDto>>.SuccessResponse(response, 200);
        }


        public ApiResponse<MunicipalityDto> GetMunicipalityById(int id)
        {
            var municipality = _unitOfWork.Municipality.GetById(id);
            if (municipality == null)
            {
                return ApiResponse<MunicipalityDto>.ErrorResponse("Municipality not found", 404);
            }
            return ApiResponse<MunicipalityDto>.SuccessResponse(_mapper.Map<MunicipalityDto>(municipality), 200);
        }

        public ApiResponse<int> CreateMunicipality(MunicipalityDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return ApiResponse<int>.ErrorResponse("Name is required", 400);
            }
            if (request.IsActive == false)
            {
                request.IsActive = true;
            }

            var municipality = _mapper.Map<Municipality>(request);

            _unitOfWork.Municipality.Create(municipality);
            _unitOfWork.Complete();

            return ApiResponse<int>.SuccessResponse(municipality.Id, 201);
        }

        public ApiResponse<bool> UpdateMunicipality(int id, MunicipalityDto municipality)
        {
            var existing = _unitOfWork.Municipality.GetById(id);
            if (existing == null)
            {
                return ApiResponse<bool>.ErrorResponse("Municipality not found", 404);
            }
            existing.Name = municipality.Name;
            existing.PostalCode = municipality.PostalCode;
            existing.IsActive = municipality.IsActive;
            _unitOfWork.Municipality.Update(existing);
            _unitOfWork.Complete();
            return ApiResponse<bool>.SuccessResponse(true, 200);
        }

        public ApiResponse<bool> DeleteMunicipality(int id)
        {
            var existing = _unitOfWork.Municipality.GetById(id);
            if (existing == null)
            {
                return ApiResponse<bool>.ErrorResponse("Municipality not found", 404);
            }
            _unitOfWork.Municipality.Delete(existing);
            _unitOfWork.Complete();
            return ApiResponse<bool>.SuccessResponse(true, 200);
        }
    }
}