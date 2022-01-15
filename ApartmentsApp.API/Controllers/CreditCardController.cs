using ApartmentsApp.API.Models;
using ApartmentsApp.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly CreditCardService _creditCardService;
        public CreditCardController(CreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpGet]
        public ActionResult<List<CreditCard>> Get()
        {
            return _creditCardService.GetAll();
        }

        [HttpGet("{id:length(24)}", Name = "GetCard")]
        public ActionResult<CreditCard> Get(string id)
        {
            var card = _creditCardService.Get(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        [HttpGet("GetMyCards/{userId}")]
        [Route("GetMyCards")]
        public ActionResult<List<CreditCard>> GetMyCards(int userId)
        {
            var cards = _creditCardService.GetMyCards(userId).ToList();
            if(cards == null)
            {
                return NotFound();
            }
            return cards;
        }

        [HttpPost]
        public ActionResult<CreditCard> Insert(CreditCard card)
        {
            if (ValidateCard(card))
            {
                _creditCardService.Create(card);
                return CreatedAtRoute("GetCard", new { id = card.Id.ToString() }, card);
            }
            return BadRequest();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, CreditCard cardModel)
        {
            var card = _creditCardService.Get(id);

            if (card == null)
            {
                return NotFound();
            }
            if (ValidateCard(cardModel))
            {
                _creditCardService.Update(id, cardModel);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var card = _creditCardService.Get(id);

            if (card == null)
            {
                return NotFound();
            }

            _creditCardService.Remove(card.Id);

            return NoContent();
        }

        [HttpGet("{id:length(24)}")]
        [Route("ValidateMyCard")]
        public bool ValidateMyCard(string id)
        {
            var card = _creditCardService.Get(id);
            return ValidateCard(card);
        }

        private bool ValidateCard(CreditCard card)
        {
            var today = new DateTime();
            if(card.CardNo.Length == 16 && card.CVC.Length == 3)
            {
                if (card.Year > today.Year)
                {
                    return true;
                }
                else if (card.Year == today.Year)
                {
                    if (card.Month > today.Month)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        [HttpPost]
        [Route("Pay")]
        public bool Pay([FromBody] PaymentModel model)
        {
            var card = _creditCardService.Get(model.cardId);
            if (ValidateCard(card))
            {
                if(card.Balance >= model.total)
                {
                    card.Balance = card.Balance - model.total;
                    _creditCardService.Update(model.cardId, card);
                    return true;
                }
            }
            return false;
        }

    }
}
