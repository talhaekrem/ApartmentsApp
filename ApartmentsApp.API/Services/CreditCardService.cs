using ApartmentsApp.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsApp.API.Services
{
    public class CreditCardService
    {
        private readonly IMongoCollection<CreditCard> _creditCards;

        public CreditCardService(ICreditCardDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _creditCards = database.GetCollection<CreditCard>(settings.CreditCardsCollectionName);
        }

        public List<CreditCard> GetAll()
        {
            return _creditCards.Find(card => true).ToList();
        }

        public CreditCard Get(string id)
        {
            return _creditCards.Find<CreditCard>(card => card.Id == id).FirstOrDefault();
        }

        public List<CreditCard> GetMyCards(int userId)
        {
            return _creditCards.Find(card => card.UserId == userId).ToList();
        }

        public CreditCard Create(CreditCard card)
        {
            _creditCards.InsertOne(card);
            return card;
        }

        public void Update(string id, CreditCard cardModel)
        {
            _creditCards.ReplaceOne(card => card.Id == id, cardModel);
        }

        public void Remove(CreditCard cardModel)
        {
            _creditCards.DeleteOne(card => card.Id == cardModel.Id);
        }

        public void Remove(string id)
        {
            _creditCards.DeleteOne(card => card.Id == id);
        }
    }
}
