using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using PublicApi.DTO.v1.CampaignDTOs;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public CampaignsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Campaigns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignDTO>>> GetCampaigns()
        {
            var campaignDTOs = await _uow.Campaigns.DTOAllAsync();
            
            return Ok(campaignDTOs);

        }

        // GET: api/Campaigns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignDTO>> GetCampaign(Guid id)
        {
            var campaign = await _uow.Campaigns.DTOFirstOrDefaultAsync(id);

            if (campaign == null)
            {
                return NotFound();
            }

            return Ok(campaign);

        }

        // PUT: api/Campaigns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampaign(Guid id, CampaignEditDTO campaignEditDTO)
        {
            if (id != campaignEditDTO.Id)
            {
                return BadRequest();
            }

            var campaign = await _uow.Campaigns.FirstOrDefaultAsync(campaignEditDTO.Id);
            if (campaign == null)
            {
                return BadRequest();
            }

            campaign.To = campaignEditDTO.To;
            campaign.Name = campaignEditDTO.Name;
            campaign.Comment = campaignEditDTO.Comment;
            
            _uow.Campaigns.Update(campaign);


            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Campaigns.ExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Campaigns
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Campaign>> PostCampaign(CampaignCreateDTO campaignCreateDTO)
        {
            var campaign = new Campaign
            {
                Id = campaignCreateDTO.Id,
                From = campaignCreateDTO.From,
                To = campaignCreateDTO.To,
                Name = campaignCreateDTO.Name,
                Comment = campaignCreateDTO.Comment
            };
            
            _uow.Campaigns.Add(campaign);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetCampaign", new { id = campaign.Id }, campaign);
        }

        // DELETE: api/Campaigns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Campaign>> DeleteCampaign(Guid id)
        {
            var campaign = await _uow.Campaigns.FirstOrDefaultAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            _uow.Campaigns.Remove(campaign);
            await _uow.SaveChangesAsync();

            return Ok(campaign);
        }
    }
}
