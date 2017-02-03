using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using notcreepy.Models;
using notcreepy.Factory; //Need to include reference to new Factory Namespace
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.IO;

namespace notcreepy.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment hostingEnv;
     
        private readonly UserFactory userFactory;
        private readonly ChallengeFactory challengeFactory;
                private readonly ToDoFactory todoFactory;


        private readonly SubmissionFactory submissionFactory;
        private readonly FollowshipFactory followshipFactory;
        public HomeController(UserFactory user, ChallengeFactory challenge, SubmissionFactory submission, FollowshipFactory followship, ToDoFactory todo, IHostingEnvironment environment) {
            userFactory = user;
            challengeFactory = challenge;
            submissionFactory = submission;
            followshipFactory = followship;
            todoFactory = todo;
            hostingEnv = environment;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("userid") == null){
                return View();
            }
            else{
                return Redirect("/wall");
            }
        }


        [HttpGetAttribute]
        [RouteAttribute("wall")]
        public IActionResult Wall(){
            if (HttpContext.Session.GetInt32("userid") == null){
                return Redirect("/");
            }
            else{
                //find the logged in user from Session
                //find the all followee submissions for wall
                //find all user submissions
                ViewBag.all_challenges = challengeFactory.FindAllApproved();
                return View();
            }
        }


        [HttpPostAttribute]
        [ValidateAntiForgeryTokenAttribute]
        [RouteAttribute("register")]
        public IActionResult RegisterMethod(User item){
            if(ModelState.IsValid){
                userFactory.Add(item);
                User user = userFactory.FindByEmail(item.email);
                HttpContext.Session.SetInt32("userid", (int)user.id);
                return Redirect("/wall");
            }
            ViewBag.registererrors = ModelState.Values;
        return View("Index");
        }


        [HttpGetAttribute]
        [RouteAttribute("register")]
        public IActionResult register(){
            return View("Register");
        }


        [HttpPostAttribute]
        [RouteAttribute("login")]
        public IActionResult Login(string email, string password){
            User user = userFactory.FindByEmail(email);
            if(user != null){
                var hasher = new PasswordHasher<User>();
                if (0 != hasher.VerifyHashedPassword(user, user.password, password)){
                    HttpContext.Session.SetInt32("userid", (int)user.id);
                    return Redirect("/wall");
                }
                else {
                    ViewBag.incorrectlogin = "Incorrect Password";
                }
            }
            else {
                ViewBag.incorrectlogin = "User does not exists!";
            }
            return View("Index");
        }

        [HttpPost]
        [RouteAttribute("upload")]
        public IActionResult UploadFiles(IList<IFormFile> files)
        {
            long size = 0;
            foreach (var file in files)
            {
                ViewBag.Photo = file;
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                                ViewBag.Test = filename;
                filename = hostingEnv.WebRootPath + $@"\{filename}";
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                    ViewBag.fs = fs;
                }
            }
            ViewBag.Message = $"{files.Count} file(s) uploaded successfully!";
            return View("Index");
        }


        [HttpPost]
        [RouteAttribute("submission")]
        public IActionResult Submission(IFormFile file, int challenge_id)
        {
            long size = 0;

                ViewBag.Photo = file;
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                  
                              ViewBag.Test = filename;

                filename = hostingEnv.WebRootPath + $@"\{filename}";
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                    ViewBag.fs = fs;
                }

                Submission new_submission = new Submission(){
                    image = filename,
                    user_id = (int)HttpContext.Session.GetInt32("userid"),
                    challenge_id = challenge_id,
                };

                submissionFactory.Add(new_submission);
            ViewBag.Message = $"file uploaded successfully!";
            return View("Index");
        }

        [HttpGetAttribute]
        [RouteAttribute("challenge/{challenge_id}")]
        public IActionResult OneChallenge(int challenge_id){
            Challenge this_challenge = challengeFactory.FindApprovedByID(challenge_id);
            ViewBag.challenge = this_challenge;
            ViewBag.submissions = submissionFactory.FindByChallenge(this_challenge);
            ViewBag.users = userFactory.FindAll();
            return View("OneChallenge");
        }

        [HttpGetAttribute]
        [RouteAttribute("explore")]
        public IActionResult Explore(){
            ViewBag.challenges = challengeFactory.FindAllApproved();
            ViewBag.users = userFactory.FindAll();
            ViewBag.submissions = submissionFactory.FindAll();
            return View("Explore");
        }



        [HttpGetAttribute]
        [RouteAttribute("challenges")]
        public IActionResult AllChallenges(){
            ViewBag.challenges = challengeFactory.FindAllApproved();
            return View("Challenges");
        }


        [HttpGetAttribute]
        [RouteAttribute("newchallenge")]
        public IActionResult NewChallenge(){
            return View("NewChallenge");
        }


        [HttpPostAttribute]
        [RouteAttribute("newchallenge")]
        public IActionResult PostNewChallenge(Challenge item){
            if(ModelState.IsValid){
                challengeFactory.Add(item);
                ViewBag.challengeaccepted = "Your suggestion has been submitted!";
            }
            ViewBag.challengeerrors = ModelState.Values;
            return View("NewChallenge");
        }


        [HttpPostAttribute]
        [RouteAttribute("challenge/{user_id}")]
        public IActionResult ChallengeSomeone(ToDo item){
            todoFactory.Add(item);
            return View("");

        }
    }
}















        // [HttpPost]
        // [RouteAttribute("upload")]
        // public IActionResult UploadFiles(IList<IFormFile> files)
        // {
        //     // long size = 0;
        //     foreach (var file in files)
        //     {
        //         byte[] image = userFactory.ConvertToBytes(file);
        //         System.Console.WriteLine(image + "******************************************");
        //         // ViewBag.Photo = file;
        //         // var filename = ContentDispositionHeaderValue
        //         //                 .Parse(file.ContentDisposition)
        //         //                 .FileName
        //         //                 .Trim('"');
        //         //                 ViewBag.Test = filename;
        //         // filename = hostingEnv.WebRootPath + $@"\{filename}";
        //         // size += file.Length;
        //         // using (FileStream fs = System.IO.File.Create(filename))
        //         // {
        //         //     file.CopyTo(fs);
        //         //     fs.Flush();
        //         //     ViewBag.fs = fs;
        //         // }
        //     }  
        //     ViewBag.Message = $"{files.Count} file(s) uploaded successfully!";
        //     return View("Index");
        // }