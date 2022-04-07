using Member.Application.Contracts.Persistance;
using Member.Application.Models;
using Member.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Member.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IMemberContext _dbContext;

        public MemberRepository(IMemberContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<ActionReturnType> CreateMember(DentalEntity dentalEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionReturnType> GetDentalInformation()
        {
            var dentalObj = await _dbContext.DentalEntity.Find(p => true).ToListAsync();
            if (dentalObj.Count > 0)
            {
                return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, dentalObj);
            }
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NotFound, dentalObj);
        }

        public async Task<ActionReturnType> GetPatients()
        {
            var patientList = await _dbContext.PatientEntity.Find(p => true).ToListAsync();
            if (patientList.Count > 0)
            {
                return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, patientList);
            }
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NotFound, "no patient exist");
        }

        public async Task<ActionReturnType> GetPatientById(string Id)
        {
            var patientObj = await _dbContext.PatientEntity.Find(p => p.Id == Id).FirstOrDefaultAsync();
            if (patientObj != null)
            {
                return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, patientObj);
            }
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NotFound, "patient not found");
        }
    }
}
