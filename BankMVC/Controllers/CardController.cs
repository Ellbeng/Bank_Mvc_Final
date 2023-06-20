using BankMVC.DTO;
using BankMVC.Models;
using BankMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BankMVC.Controllers
{
    public class CardController:Controller
    {
       
        private readonly ITransactionService _transactionService;
        private readonly IConfiguration _configuration;
        private readonly ICardService _cardService;

        public CardController( IConfiguration configuration, ITransactionService transactionService, ICardService cardService)
        {
            _configuration = configuration;
            _transactionService = transactionService;
            _cardService = cardService;
        }
        public IActionResult CardInformation()
        {

            return View();
        }

        [HttpPost]
        public async Task <JsonResult> CardForm(CardDto card, TransactionDto transaction)
        {
            var cardmodel = new CardDto();
            cardmodel.CardNumber = card.CardNumber;
            cardmodel.ExpirationMonth = card.ExpirationMonth;
            cardmodel.ExpirationYear = card.ExpirationYear;
            cardmodel.CVV = card.CVV;

            var user = _cardService.CheckCardUser(cardmodel);
            var bankTransaction = new BankTransaction();
            bankTransaction.UserId = user.UserId;
            bankTransaction.PaymentType = transaction.PaymentType;
            bankTransaction.Currency = transaction.Currency;
            bankTransaction.Status =Convert.ToInt16(transaction.Status);
            bankTransaction.CreateDate = DateTime.Now;
            bankTransaction.Amount = Convert.ToDecimal(transaction.Amount);
            _transactionService.InsertTransaction(bankTransaction);



            var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"Id", transaction.Id.ToString() },
                    {"Amount", transaction.Amount.ToString()},
                    {"Status", transaction.Status.ToString()},
                    {"PaymentType", transaction.PaymentType.ToString() }
     
                });
                var client = new HttpClient();
            

                var uri = transaction.CallbackUrl;

                var response = await client.PostAsync(uri, content);

                string textResult = await response.Content.ReadAsStringAsync();

               
                
            


            return Json(new { callBackUrl=textResult });
        }
        [HttpPost]
        public IActionResult RedirectToBank(TransactionDto transaction)
        {
            var bankTransaction = new TransactionDto();
            bankTransaction.Id = transaction.Id;
            bankTransaction.PaymentType = transaction.PaymentType;
            bankTransaction.Currency = transaction.Currency;
            bankTransaction.Status = transaction.Status;
            bankTransaction.CreateDate = DateTime.Now;
            bankTransaction.Amount = transaction.Amount;
            bankTransaction.CallbackUrl = transaction.CallbackUrl;

            string bankFormFillLink = _cardService.GenerateBankFormFillLink(bankTransaction);
            return Ok(bankFormFillLink);
        }





        [HttpPost]
        public JsonResult InsertTransaction(CardDto card)
        {
            var user = _cardService.CheckCardUser(card);
            //var transaction = new BankTransaction();
            //transaction.UserId = user.UserId;
            //transaction.PaymentType = transactionDto.PaymentType;
            //transaction.Status = 1;
            //transaction.Currency = "GEL";
            //if (transaction.PaymentType == "Deposit") transaction.Amount = transactionDto.Amount;
            //else if (transaction.PaymentType == "Withdraw") transaction.Amount = transactionDto.Amount * -1;
            //transaction.CreateDate = DateTime.Now;
            //transaction.Status = 1;
            //_transactionService.InsertTransaction(transaction);


            return Json(new { success = true });


        }









    }
}
