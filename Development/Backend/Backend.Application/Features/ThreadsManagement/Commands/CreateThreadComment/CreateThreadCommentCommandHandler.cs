using Backend.Domain.Exceptions;
using Backend.Application.Repositories;
using MediatR;
using Backend.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Backend.Domain.Models.ThreadsManagement;
using Backend.Domain.Models.TalesManagement.Entities;
using Backend.Application.Features.TalesManagement.Commands.CreateTaleComment;
using AutoMapper.Features;
using System.IO.Pipelines;
using System.Xml;
using System;

namespace Backend.Application.Features.ThreadsManagement.Commands.CreateThreadComment
{
    public class CreateThreadCommentCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger,
        IFileHandler fileHandler, IWebHostEnvironment webHostEnvironment)
        : IRequestHandler<CreateThreadCommentCommand, Result<CreateThreadCommentResponse>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
        private readonly IFileHandler _fileHandler = fileHandler;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<Result<CreateThreadCommentResponse>> Handle(CreateThreadCommentCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new CreateThreadCommentCommandValidator();
            
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (validatorResult is not null && validatorResult.IsValid == false)
            {
                var errors = string.Join(". ", (validatorResult.Errors.Select(x => x.ErrorMessage).ToList()));

                var errorResponse = new Error(Code: StatusCodes.Status500InternalServerError,
                              Title: "Validation Errors",
                              Description: $"The following errors occured: '{errors}'.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;
            }

            Threads? threads = await _unitOfWork.ThreadsRepository.GetThreadById(request.ThreadId);

            //Checks if threads exists
            if (threads is null)
            {
                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
                                              Title: "Thread Not Found",
                                              Description: $"Thread with Id: '{request.ThreadId}' was not found.");

                _errorLogger.LogError($"Thread with Id: '{request.ThreadId}' was not found.");

                return errorResponse;
            }

            //save comment
            var result = threads.SaveComment(
                request.Details,
                request.AccountId,
                await _unitOfWork.UserRepository.GetUsernameById(request.AccountId)
                );

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //await CreateSeedComments();


            //Add user to repository
            _unitOfWork.RepositoryFactory<Threads>().Update(threads);

            //Save changes
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                //Return success
                return new CreateThreadCommentResponse
                {
                    Comment = await _unitOfWork.ThreadsRepository.LoadThreadComment(request.AccountId, result.Value)
                };

                //return new CreateThreadCommentResponse();


            }
            catch (Exception ex)
            {

                 _errorLogger.LogError(ex);

                return new Error(Code: StatusCodes.Status500InternalServerError,
                                              Title: "Database Error",
                                              Description: ex.Message);
            }
      
        }


//        private async Task CreateSeedComments()
//        {

//            List<string> commentators = new()
// {
//     "4c6d75c5-2db6-45f8-8a7f-9a448f8b9eb0",
//     "54c35dbb-1b4d-4d46-bc6c-dfddf11859d7",
//     "5550c3b2-d51b-4478-8159-214af4387398",
//     "60d10457-52c1-4f71-96ac-59349b13d52f",
//     "62a464b2-69f3-41f0-8f34-23345a1174cf",
//     "98cc2587-9cae-42c0-8a34-5e35d47a3ebc",
//     "a2010bbb-5c79-4222-9cda-7839fc0f41e4",
//     "c7b1b87f-497f-49c8-8367-c329326af908",
//     "eefaab51-466f-4a98-a8da-49197a2dff62",
//     "fb3fb39c-5eea-491e-97c5-ed77f54c53a2",
// };

//            List<string> commentators1Layer = new()
// {
//     "4c6d75c5-2db6-45f8-8a7f-9a448f8b9eb0",
//     "54c35dbb-1b4d-4d46-bc6c-dfddf11859d7",
//     "5550c3b2-d51b-4478-8159-214af4387398",
//     "60d10457-52c1-4f71-96ac-59349b13d52f",
//     "62a464b2-69f3-41f0-8f34-23345a1174cf",
//     "98cc2587-9cae-42c0-8a34-5e35d47a3ebc",
//     "a2010bbb-5c79-4222-9cda-7839fc0f41e4",
//     "c7b1b87f-497f-49c8-8367-c329326af908",
//     "eefaab51-466f-4a98-a8da-49197a2dff62",
//     "fb3fb39c-5eea-491e-97c5-ed77f54c53a2",
//"4c6d75c5-2db6-45f8-8a7f-9a448f8b9eb0",
//     "54c35dbb-1b4d-4d46-bc6c-dfddf11859d7",
//     "5550c3b2-d51b-4478-8159-214af4387398",
//     "60d10457-52c1-4f71-96ac-59349b13d52f",
//     "62a464b2-69f3-41f0-8f34-23345a1174cf",
//     "98cc2587-9cae-42c0-8a34-5e35d47a3ebc",
//     "a2010bbb-5c79-4222-9cda-7839fc0f41e4",
//     "c7b1b87f-497f-49c8-8367-c329326af908",
//     "eefaab51-466f-4a98-a8da-49197a2dff62",
//     "fb3fb39c-5eea-491e-97c5-ed77f54c53a2",
//"4c6d75c5-2db6-45f8-8a7f-9a448f8b9eb0",
//     "54c35dbb-1b4d-4d46-bc6c-dfddf11859d7",
//     "5550c3b2-d51b-4478-8159-214af4387398",
//     "60d10457-52c1-4f71-96ac-59349b13d52f",
//     "62a464b2-69f3-41f0-8f34-23345a1174cf",
//     "98cc2587-9cae-42c0-8a34-5e35d47a3ebc",
//     "a2010bbb-5c79-4222-9cda-7839fc0f41e4",
//     "c7b1b87f-497f-49c8-8367-c329326af908",
//     "eefaab51-466f-4a98-a8da-49197a2dff62",
//     "fb3fb39c-5eea-491e-97c5-ed77f54c53a2",
// };

//            List<string> commentators2Layer = new()
// {

//     "54c35dbb-1b4d-4d46-bc6c-dfddf11859d7",
//"4c6d75c5-2db6-45f8-8a7f-9a448f8b9eb0",
//     "60d10457-52c1-4f71-96ac-59349b13d52f",
//"5550c3b2-d51b-4478-8159-214af4387398",
//     "98cc2587-9cae-42c0-8a34-5e35d47a3ebc",
//"62a464b2-69f3-41f0-8f34-23345a1174cf",
//     "c7b1b87f-497f-49c8-8367-c329326af908",
//"a2010bbb-5c79-4222-9cda-7839fc0f41e4",
//     "fb3fb39c-5eea-491e-97c5-ed77f54c53a2",
// "eefaab51-466f-4a98-a8da-49197a2dff62",
// "54c35dbb-1b4d-4d46-bc6c-dfddf11859d7",
//"4c6d75c5-2db6-45f8-8a7f-9a448f8b9eb0",
//     "60d10457-52c1-4f71-96ac-59349b13d52f",
//"5550c3b2-d51b-4478-8159-214af4387398",
//     "98cc2587-9cae-42c0-8a34-5e35d47a3ebc",
//"62a464b2-69f3-41f0-8f34-23345a1174cf",
//     "c7b1b87f-497f-49c8-8367-c329326af908",
//"a2010bbb-5c79-4222-9cda-7839fc0f41e4",
//     "fb3fb39c-5eea-491e-97c5-ed77f54c53a2",
// "eefaab51-466f-4a98-a8da-49197a2dff62",
// "54c35dbb-1b4d-4d46-bc6c-dfddf11859d7",
//"4c6d75c5-2db6-45f8-8a7f-9a448f8b9eb0",
//     "60d10457-52c1-4f71-96ac-59349b13d52f",
//"5550c3b2-d51b-4478-8159-214af4387398",
//     "98cc2587-9cae-42c0-8a34-5e35d47a3ebc",
//"62a464b2-69f3-41f0-8f34-23345a1174cf",
//     "c7b1b87f-497f-49c8-8367-c329326af908",
//"a2010bbb-5c79-4222-9cda-7839fc0f41e4",
//     "fb3fb39c-5eea-491e-97c5-ed77f54c53a2",
// "eefaab51-466f-4a98-a8da-49197a2dff62",

// };

//            List<string> commentators3Layer = new()
// {


//     "5550c3b2-d51b-4478-8159-214af4387398",
//     "60d10457-52c1-4f71-96ac-59349b13d52f",
//"4c6d75c5-2db6-45f8-8a7f-9a448f8b9eb0",
//     "54c35dbb-1b4d-4d46-bc6c-dfddf11859d7",
//     "a2010bbb-5c79-4222-9cda-7839fc0f41e4",
//     "c7b1b87f-497f-49c8-8367-c329326af908",
// "62a464b2-69f3-41f0-8f34-23345a1174cf",
//     "98cc2587-9cae-42c0-8a34-5e35d47a3ebc",
//     "eefaab51-466f-4a98-a8da-49197a2dff62",
//     "fb3fb39c-5eea-491e-97c5-ed77f54c53a2",
// "5550c3b2-d51b-4478-8159-214af4387398",
//     "60d10457-52c1-4f71-96ac-59349b13d52f",
//"4c6d75c5-2db6-45f8-8a7f-9a448f8b9eb0",
//     "54c35dbb-1b4d-4d46-bc6c-dfddf11859d7",
//     "a2010bbb-5c79-4222-9cda-7839fc0f41e4",
//     "c7b1b87f-497f-49c8-8367-c329326af908",
// "62a464b2-69f3-41f0-8f34-23345a1174cf",
//     "98cc2587-9cae-42c0-8a34-5e35d47a3ebc",
//     "eefaab51-466f-4a98-a8da-49197a2dff62",
//     "fb3fb39c-5eea-491e-97c5-ed77f54c53a2",
// "5550c3b2-d51b-4478-8159-214af4387398",
//     "60d10457-52c1-4f71-96ac-59349b13d52f",
//"4c6d75c5-2db6-45f8-8a7f-9a448f8b9eb0",
//     "54c35dbb-1b4d-4d46-bc6c-dfddf11859d7",
//     "a2010bbb-5c79-4222-9cda-7839fc0f41e4",
//     "c7b1b87f-497f-49c8-8367-c329326af908",
// "62a464b2-69f3-41f0-8f34-23345a1174cf",
//     "98cc2587-9cae-42c0-8a34-5e35d47a3ebc",
//     "eefaab51-466f-4a98-a8da-49197a2dff62",
//     "fb3fb39c-5eea-491e-97c5-ed77f54c53a2",

// };



//            //1 - attack of the paper tigers

//            List<string> tigers_1 = new()
//{
//    "A fascinating exploration of AI! The article presents a balanced perspective, highlighting both its transformative potential and the ethical concerns surrounding its misuse",

//"A well-rounded discussion! The article forces readers to think critically about how AI can enhance productivity while ensuring it doesn’t fall into the wrong hands",

//"A compelling read! AI offers incredible opportunities in healthcare, automation, and accessibility, but its risks—including bias and misuse—must be carefully managed",

//"This article expertly discusses the dual nature of AI! While it can revolutionize industries, unchecked use could lead to dangerous consequences",

//"A must-read piece! The author rightly points out that responsible AI development is key to ensuring harmony between humans and machines",

//"A refreshing take on AI! Instead of focusing solely on fear or hype, the article examines both the bright and dark sides of artificial intelligence",

//"The discussion on AI ethics is timely and necessary! Regulation and oversight are crucial to prevent AI from being exploited for harmful purposes",

//"The article raises important concerns! AI must be designed with human values in mind to ensure fairness, transparency, and accountability",

//"A thought-provoking piece! AI’s ability to process vast amounts of data can accelerate progress, but without ethical safeguards, it could enable widespread misinformation",

//"A balanced and insightful discussion! AI can amplify human potential, but proper governance is needed to prevent abuses",
//};

//            List<string> tigers_2 = new()
//{
//  "A fascinating read! AI’s ability to improve healthcare, automate industries, and enhance accessibility is undeniable, but ethical concerns must be addressed",

//"This article wisely discusses AI’s risks alongside its benefits! A balanced approach is necessary to maximize AI’s potential while minimizing harm",

//"A must-read discussion! The article highlights how AI can empower humanity while also cautioning against unchecked development",

//"A smart exploration of AI’s future! Responsible regulation and ethical frameworks will be essential to ensure AI serves humanity rather than exploits it",

//"A compelling analysis! AI’s efficiency is unmatched, but human oversight must be prioritized to prevent errors and bias in decision-making",

//"The discussion on AI misuse is crucial! AI tools can revolutionize industries, but when wielded by bad actors, they could pose serious threats",

//"The article provides an excellent breakdown of AI’s impact! Finding a balance between innovation and responsible governance will shape the future",

//"A fantastic take on AI’s evolution! The article rightly argues that human collaboration with AI is key to ensuring its benefits outweigh its risks",

//"A refreshingly realistic discussion! AI is neither inherently good nor bad—it’s a tool, and its impact depends on how we use it",

//"A nuanced perspective! AI can bring revolutionary change, but regulatory frameworks must be implemented to prevent ethical violations",

//"An insightful discussion! AI’s impact on job markets, automation, and productivity is exciting, but it must be developed responsibly",

//"A well-researched article! AI has the power to solve complex problems, but governance is necessary to prevent unintended consequences",

//"A deeply relevant piece! As AI continues advancing, society must proactively address its potential risks while embracing its benefits",

//"A balanced critique of AI’s role in society! The article encourages thoughtful discourse rather than fearmongering or blind optimism",

//"An engaging read! AI can complement human capabilities, but caution is necessary to prevent misuse by those seeking power",

//"A smart take on AI! Ethical concerns must be at the forefront of technological advancements to ensure sustainable growth",

//"A compelling call to action! AI developers and policymakers must work together to ensure AI remains a positive force for society",

//"A thought-provoking analysis! AI must be integrated thoughtfully into industries, with clear ethical guidelines to prevent harm",

//"A necessary conversation! AI can accelerate progress, but society must remain vigilant to avoid unintended consequences",

//"A brilliant exploration of AI’s dual nature! Technological advancements must be paired with strong ethical safeguards to protect humanity",

//"A must-read discussion! AI can improve efficiency, but without proper safeguards, it could lead to unintended disruptions",

//"The article wisely emphasizes AI’s role in shaping the future! Ethical considerations must be prioritized alongside innovation",

//"A fresh take on AI! Technology is never inherently dangerous, but poor governance can lead to harmful consequences",

//"A well-balanced discussion! AI must be carefully designed to serve humanity, ensuring it remains a tool for good rather than exploitation",

//"A nuanced examination of AI’s risks! It’s vital to address ethical concerns while ensuring AI continues improving lives",

//"A fascinating dive into AI ethics! The article rightly explores how human oversight must remain central in AI development",

//"An insightful piece! While AI can enhance decision-making, it must remain transparent and accountable to prevent misuse",

//"A smart critique! AI’s ability to automate tasks is revolutionary, but industries must adapt responsibly to avoid disruptions",

//"A compelling breakdown of AI’s benefits! Ethical AI use can transform industries, but unchecked development could lead to challenges",

//"A refreshingly practical discussion! AI’s role in society must be managed thoughtfully to ensure long-term harmony between humans and technology",
//};

//            List<string> tigers_3 = new()
//{
//   "A deeply insightful analysis! AI’s role in society must be shaped by ethical considerations to ensure long-term benefits",

//"A well-researched perspective! AI can enhance human capabilities, but governance and transparency are essential",

//"The article wisely addresses AI’s dual impact—technology is a powerful tool, but its use must be carefully managed",

//"A necessary discussion! AI’s ability to automate tasks is revolutionary, but industries must adapt responsibly to prevent unintended consequences",

//"A balanced and refreshing take on AI! While fears exist, regulation and innovation can ensure AI remains a force for good",

//"A compelling call for ethical AI development! The article presents a practical approach to harnessing AI’s potential while mitigating risks",

//"A smart critique of AI’s rapid growth! While automation is beneficial, industries must ensure a smooth transition for workers",

//"An insightful piece! AI must be built with fairness and accountability to prevent biases and exploitation",

//"A fascinating discussion on AI’s role in daily life! Its potential to improve education, healthcare, and security is immense, but ethical oversight remains crucial",

//"A must-read article! It explores how responsible AI use can harmonize human progress with technological advancement",

//"The article raises a crucial point—AI is only as ethical as the humans developing it, making governance key",

//"A compelling discussion! AI’s ability to improve efficiency is undeniable, but its impact must be monitored to prevent exploitation",

//"A well-rounded examination of AI’s future! While automation is helpful, industries must integrate AI thoughtfully",

//"A fantastic exploration of AI’s ethical landscape! Developers and policymakers must collaborate to ensure AI remains a positive force",

//"A smart take on AI’s evolution! Ethical considerations must be central to prevent unintended harm",

//"The article wisely highlights AI’s potential! While risks exist, innovation and regulation can work together to ensure safe development",

//"A nuanced approach to AI’s impact! Ethical AI development must prioritize human welfare over unchecked technological growth",

//"A balanced discussion! AI can accelerate productivity but must be controlled to prevent harmful applications",

//"A compelling argument for AI regulation! While automation improves efficiency, responsible governance is essential",

//"A must-read perspective! AI can drive global transformation, but its risks must not be overlooked",

//"An insightful analysis! AI must be developed responsibly to ensure fairness and transparency",

//"A sharp and necessary critique! While AI enhances daily life, it must not be used irresponsibly",

//"A fantastic read! The article wisely considers AI’s advantages while warning against potential pitfalls",

//"A fresh perspective on AI’s influence! Ethical AI use must prioritize human interests and global well-being",

//"A bold and thought-provoking critique! AI must be designed with ethical considerations to prevent misuse",

//"A well-balanced discussion! While AI drives progress, its governance must ensure fairness and accountability",

//"A timely and relevant article! The ethical implications of AI must be addressed alongside technological advancements",

//"A strong argument for responsible AI development! Human oversight must remain central to technological growth",

//"A compelling breakdown of AI’s risks and benefits! It presents a balanced view of innovation and responsibility",

//"A necessary discussion! AI can transform industries, but its ethical concerns must be carefully managed",
//};

//            List<string> tigers_4 = new()
//{
//  "A well-articulated analysis! AI must be developed with ethical safeguards to ensure fairness and accountability",

//"A fascinating take on AI’s dual impact! While it enhances efficiency, governance remains a critical factor",

//"A balanced discussion! The article rightly explores AI’s benefits while cautioning against misuse",

//"A must-read exploration of AI ethics! Oversight and regulation must evolve alongside technological advancements",

//"A compelling discussion on AI’s potential! While its efficiency is remarkable, ethical concerns must drive its development",

//"A fantastic critique of AI’s role in society! Thoughtful innovation must ensure AI remains beneficial for humanity",

//"A sharp and necessary argument! AI must prioritize fairness and accountability in all applications",

//"A timely and insightful perspective! AI’s ability to shape the future requires careful ethical considerations",

//"A smart discussion on AI’s ethical landscape! Responsible governance is key to ensuring AI enhances human progress",

//"A bold and necessary critique! AI must be designed for ethical use, ensuring harmony between humans and technology",

//"A fascinating dive into AI’s future! Responsible development will dictate its success in society",

//"A fantastic exploration of AI’s possibilities! While automation is powerful, ethical safeguards must not be neglected",

//"A well-structured analysis! AI can drive innovation but requires careful monitoring to prevent harm",

//"A sharp critique of AI’s ethical concerns! While it enhances efficiency, oversight is necessary to ensure responsible use",

//"A compelling perspective! AI’s ability to improve daily life is immense, but governance remains crucial",

//"A must-read discussion! AI’s evolution must be accompanied by ethical responsibility to prevent misuse",

//"A powerful exploration of AI’s influence! Responsible innovation can ensure AI remains a force for good",

//"A fresh perspective on AI’s role in society! Ethical use must be prioritized over rapid technological expansion",

//"A balanced and insightful discussion! AI’s impact must be managed carefully to ensure sustainability",

//"A bold critique of AI’s development! Human oversight remains critical to preventing unintended harm",

//"A thought-provoking examination of AI’s ethical concerns! While transformative, it must not be abused",

//"A strong argument for AI’s responsible use! While technology accelerates progress, governance must evolve with it",

//"A fascinating take on AI’s future! Ethical safeguards must be central to its development",

//"A compelling breakdown of AI’s influence! While automation is powerful, responsible use must guide its growth",

//"A sharp and engaging critique! AI’s evolution must prioritize fairness and accountability in all industries",

//"A well-balanced discussion! While AI offers incredible advancements, its regulation must be strengthened",

//"A necessary conversation on AI ethics! Responsible AI development must prioritize human well-being",

//"A must-read analysis! AI must be structured with ethical safeguards to prevent bias and exploitation",

//"A smart discussion on AI’s dual impact! While automation is revolutionary, proper governance must guide its future",

//"A bold and insightful perspective! AI’s development must be paired with ethical responsibility to ensure global harmony",
//};

//            Threads? thread_tiger = await _unitOfWork.ThreadsRepository.GetThreadById(Guid.Parse("440fd82d-92a1-4b85-a8f7-a7bc90b7b626"));

//            if (thread_tiger is not null)
//            {
//                //create the comments
//                for (var i = 0; i < tigers_2.Count; i++)
//                {

//                    var resulti = thread_tiger.SaveComment(
//                        tigers_2[i],
//                        Guid.Parse(commentators1Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
//                        );

//                    //for the first comment in the list
//                    if (i == 0)
//                    {
//                        for (var j = 0; j < tigers_1.Count; j++)
//                        {
//                            thread_tiger.SaveResponse(
//                                resulti.Value,
//                                tigers_1[j],
//                                Guid.Parse(commentators[j]),
//                                Guid.Parse(commentators1Layer[i]),
//                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
//                        }
//                    }

//                    var resultk = thread_tiger.SaveResponse(
//                        resulti.Value,
//                        tigers_3[i],
//                        Guid.Parse(commentators2Layer[i]),
//                         Guid.Parse(commentators1Layer[i]),
//                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

//                    var resultl = thread_tiger.SaveResponse(
//                        resultk.Value,
//                        tigers_4[i],
//                        Guid.Parse(commentators3Layer[i]),
//                        Guid.Parse(commentators2Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

//                }

//                _unitOfWork.RepositoryFactory<Threads>().Update(thread_tiger);

//            }



//            //2 - art of the deal

//            List<string> deals_1 = new()
//{
//    "A compelling analysis! The article rightly questions whether true judicial impartiality can exist in a system where the selection process is inherently political",

//"A thought-provoking discussion! The judiciary is expected to remain neutral, yet its members are appointed by partisan leaders—can reform truly ensure independence?",

//"A bold critique of judicial selection! The article forces readers to consider whether reforms could reduce political influence in judicial appointments",

//"A necessary conversation! While judges strive for impartiality, the process by which they are appointed suggests underlying biases",

//"A fascinating take on separation of powers! The article presents a strong case for examining judicial neutrality in a highly partisan system",

//"A smart exploration of judicial independence! If legislative and executive branches are openly partisan, should the judiciary maintain neutrality at all costs?",

//"A compelling challenge to legal tradition! The article questions whether a truly impartial judiciary can exist when judges are appointed through partisan processes",

//"A bold examination of judicial integrity! The piece highlights how political appointments create an unavoidable conflict in judicial neutrality",

//"A well-argued discussion on governance reform! The article explores whether structural changes could enhance judicial impartiality",

//"A necessary debate on legal fairness! If judicial appointments are influenced by politics, can courts truly serve as neutral arbiters?",
//};

//            List<string> deals_2 = new()
//{
//  "A sharp critique of judicial selection! The article highlights how political affiliations indirectly shape court decisions despite expectations of neutrality",

//"A must-read discussion! If judges are chosen through partisan processes, can they ever truly function without bias?",

//"A fascinating take on judicial independence! The article questions whether reform can effectively reduce ideological influence in legal rulings",

//"A smart examination of governance structure! It forces readers to reflect on whether neutrality can be maintained when political dynamics shape appointments",

//"A necessary challenge to legal tradition! The article highlights the contradiction between judicial impartiality and politically-driven selection methods",

//"A bold call for reform! It argues that ensuring neutrality requires reevaluating how judges are appointed in the first place",

//"A compelling discussion on institutional integrity! If executive and legislative branches operate with clear partisan bias, should the judiciary be exempt from similar scrutiny?",

//"A well-researched critique! The article presents strong reasoning for reassessing whether partisan appointments compromise judicial independence",

//"A fascinating debate on governance reform! The discussion questions whether judicial impartiality is possible in a system that inherently favors political ideology",

//"A striking examination of legal fairness! If judges are selected based on political preferences, how can courts remain unbiased?",

//"A thought-provoking challenge to judicial neutrality! The article urges readers to reconsider whether structural changes are needed to protect court integrity",

//"A smart critique of political influence in judicial decisions! The discussion raises concerns about whether neutrality can exist without reform",

//"A compelling breakdown of governance contradictions! It questions whether separation of powers truly prevents ideological bias in courts",

//"A well-structured legal discussion! The article presents strong arguments for evaluating judicial appointments with greater transparency",

//"A fascinating perspective on judicial selection! If politics shape judge appointments, can legal decisions remain unaffected?",

//"A bold critique of partisanship in governance! The article examines how judicial neutrality is expected while political bias influences every other branch",

//"A thought-provoking analysis! It challenges the assumption that judicial independence can be upheld when selection processes favor political ideology",

//"A fresh take on legal impartiality! The article highlights the challenges of separating politics from judicial appointments",

//"A necessary conversation! The discussion presents compelling arguments for reforming court selection to prevent partisan influence",

//"A powerful breakdown of governance contradictions! It forces readers to consider whether judicial neutrality is more theoretical than practical",

//"A smart take on judicial reform! The article suggests that reducing political influence in appointments may be the key to true neutrality",

//"A sharp critique of selection methods! It questions whether ideological affiliations inevitably seep into judicial decision-making",

//"A bold legal debate! The article presents strong reasoning for reevaluating how judges are nominated and approved",

//"A fascinating discussion on separation of powers! If judicial appointments are inherently political, can courts ever function impartially?",

//"A necessary exploration of governance fairness! The article argues that political influence must be minimized in legal rulings",

//"A compelling breakdown of judicial expectations! It highlights the unrealistic assumption that judges remain unaffected by the political environment",

//"A fresh legal perspective! The discussion questions whether current judicial appointment processes ensure true impartiality",

//"A striking argument for reform! The article presents compelling evidence that political bias affects court structures more than we acknowledge",

//"A well-balanced critique! The piece questions whether neutrality can exist within a judicial system shaped by partisan interests",

//"A thought-provoking challenge to governance structures! The article explores the tension between legal independence and political influence",

//};

//            List<string> deals_3 = new()
//{
//   "A powerful argument for judicial reform! The article questions whether neutrality can exist when judicial appointments are shaped by political interests",

//"A necessary discussion! It highlights the contradiction between expecting impartial rulings while allowing political influence in the selection process",

//"A fascinating legal debate! The article challenges whether true judicial independence is achievable under current governance structures",

//"A smart critique of partisan judicial appointments! It forces readers to consider whether reform could enhance fairness in legal rulings",

//"A bold challenge to legal tradition! The article examines whether courts can remain neutral in a system where other branches openly operate with bias",

//"A thought-provoking discussion! If judges are expected to be impartial, should their selection process reflect that same standard?",

//"A compelling exploration of judicial ethics! The article questions whether judges can ever truly separate themselves from political influence",

//"A striking critique of governance contradictions! It forces readers to confront whether separation of powers is effectively upheld",

//"A well-balanced examination of judicial independence! The discussion explores whether neutrality is an ideal rather than a practical reality",

//"A fresh perspective on legal fairness! The article presents strong reasoning for reevaluating judicial appointment procedures",

//"A sharp critique of governance inconsistencies! It questions whether judicial reform could limit partisan influence in court rulings",

//"A bold call for reevaluating judicial impartiality! The article presents a compelling argument that selection processes inherently shape legal outcomes",

//"A fascinating discussion on separation of powers! The article forces readers to assess whether courts can remain neutral in a politically charged system",

//"A well-researched legal breakdown! It highlights how partisan judicial appointments create long-term biases in legal interpretations",

//"A smart analysis of judicial selection! The discussion explores whether transparency in appointments could enhance impartiality",

//"A necessary critique of judicial expectations! If judges are expected to remain neutral, should appointment methods reflect that standard?",

//"A compelling reflection on legal governance! The article forces reconsideration of how courts maintain independence",

//"A striking challenge to judicial neutrality! It presents strong reasoning for why political bias inevitably affects the judiciary",

//"A bold perspective on governance contradictions! The article questions whether courts can ever function free from political influence",

//"A fresh take on judicial reform! It examines whether modifications to selection processes could enhance fairness in legal decision-making",

//"A powerful argument for reevaluating judicial selection! The article highlights inconsistencies in governance expectations",

//"A deeply insightful legal discussion! It explores how political influences affect court decisions despite expectations of neutrality",

//"A fascinating breakdown of judicial independence! The article presents strong reasoning for why selection processes require reform",

//"A necessary debate on governance contradictions! The discussion challenges whether courts can truly function without partisan influence",

//"A smart perspective on judicial appointments! It raises key questions about how political dynamics shape court rulings",

//"A well-balanced legal exploration! The article challenges the assumption that judicial impartiality can exist in a politically charged environment",

//"A bold take on court neutrality! The discussion urges readers to reevaluate how partisan interests impact legal fairness",

//"A thought-provoking challenge to governance structures! The article forces reconsideration of whether current judicial selection processes ensure independence",

//"A compelling legal breakdown! It examines whether courts can uphold fairness despite political influences",

//"A strong call for judicial transparency! The article presents a powerful argument for reevaluating selection methods",
//};

//            List<string> deals_4 = new()
//{
//  "A fascinating examination of judicial impartiality! It forces reconsideration of how political biases shape court rulings",

//"A necessary debate on governance reform! The discussion urges policymakers to reassess judicial selection strategies",

//"A bold critique of legal tradition! The article highlights inconsistencies in judicial neutrality under a partisan system",

//"A smart analysis of judicial selection! It questions whether reform efforts can effectively reduce political bias",

//"A compelling call for judicial fairness! The article presents strong reasoning for reevaluating governance structures",

//"A fresh legal perspective! It challenges whether judicial appointments should be entirely removed from political influence",

//"A thought-provoking take on governance contradictions! The discussion explores whether courts can realistically maintain neutrality",

//"A powerful argument for reevaluating judicial ethics! The article urges reconsideration of how judges are chosen",

//"A fascinating discussion on political bias in judicial rulings! It forces reflection on whether reform is necessary",

//"A must-read perspective! The article presents a nuanced debate on judicial independence versus political influence",

//"A well-researched challenge to judicial selection processes! It questions whether reforms could effectively limit partisan impact",

//"A striking critique of legal fairness! The discussion presents strong reasoning for modifying judicial appointment procedures",

//"A smart perspective on governance reform! The article highlights inconsistencies in separation of powers expectations",

//"A bold call for transparency in judicial appointments! The discussion urges reconsideration of current selection structures",

//"A compelling argument for court independence! The article presents key questions about whether neutrality can be upheld",

//"A fascinating debate on judicial ethics! It challenges the notion that legal rulings remain unaffected by political bias",

//"A thought-provoking exploration of judicial neutrality! The discussion presents compelling arguments for reform",

//"A powerful breakdown of governance contradictions! It forces reevaluation of judicial fairness principles",

//"A striking legal critique! The article highlights challenges in maintaining impartiality under a partisan selection system",

//"A fresh perspective on legal fairness! It questions whether transparency can eliminate political bias in court rulings",

//"A compelling discussion on judicial integrity! The article presents strong reasoning for reassessing selection methods",

//"A bold challenge to governance structures! It forces reconsideration of whether judicial reform is necessary",

//"A smart examination of political influence in court decisions! The discussion presents key concerns about governance fairness",

//"A fascinating exploration of separation of powers! The article challenges assumptions about judicial neutrality",

//"A necessary conversation on governance ethics! The discussion urges policymakers to reassess judicial appointment strategies",

//"A well-researched critique of judicial fairness! It highlights governance inconsistencies in partisan appointments",

//"A bold argument for court reform! The article questions whether neutral judicial rulings are achievable in a politically charged system",

//"A thought-provoking discussion on governance contradictions! The article presents strong reasoning for reassessing judicial selection procedures",

//"A compelling take on judicial impartiality! The discussion urges reconsideration of court independence principles",

//"A necessary debate on judicial selection processes! The article presents key arguments for reassessing governance fairness",
//};

//            Threads? thread_deal = await _unitOfWork.ThreadsRepository.GetThreadById(Guid.Parse("bb454229-bf8f-481f-9a30-edba7a2957f3"));

//            if (thread_deal is not null)
//            {
//                //create the comments
//                for (var i = 0; i < deals_2.Count; i++)
//                {

//                    var resulti = thread_deal.SaveComment(
//                        deals_2[i],
//                        Guid.Parse(commentators1Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
//                        );

//                    //for the first comment in the list
//                    if (i == 0)
//                    {
//                        for (var j = 0; j < deals_1.Count; j++)
//                        {
//                            thread_deal.SaveResponse(
//                                resulti.Value,
//                                deals_1[j],
//                                Guid.Parse(commentators[j]),
//                                Guid.Parse(commentators1Layer[i]),
//                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
//                        }
//                    }

//                    var resultk = thread_deal.SaveResponse(
//                        resulti.Value,
//                        deals_3[i],
//                        Guid.Parse(commentators2Layer[i]),
//                         Guid.Parse(commentators1Layer[i]),
//                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

//                    var resultl = thread_deal.SaveResponse(
//                        resultk.Value,
//                        deals_4[i],
//                        Guid.Parse(commentators3Layer[i]),
//                        Guid.Parse(commentators2Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

//                }

//                _unitOfWork.RepositoryFactory<Threads>().Update(thread_deal);

//            }

            
//            //3 - king of the beasts

//            List<string> kings_1 = new()
//{
//    "A bold perspective! The article challenges the urgency surrounding climate change, arguing that nature ultimately self-regulates",

//"A thought-provoking discussion! While Earth has survived past extinctions, the article raises the question—does human activity accelerate harmful changes?",

//"A fascinating take on environmental resilience! The article highlights nature’s ability to adapt but avoids addressing the ethical responsibility of human actions",

//"A compelling challenge to mainstream climate concerns! The argument that nature will find balance is intriguing, but does that justify ignoring active environmental damage?",

//"A necessary debate! The article forces readers to reconsider whether nature’s survival automatically means humanity’s survival",

//"A fresh perspective on planetary endurance! While dinosaurs lived for millions of years, the question remains—does human impact differ from natural cycles?",

//"A smart critique of environmental alarmism! The article suggests nature is self-correcting, but does that excuse neglecting proactive solutions?",

//"A fascinating exploration of Earth’s longevity! The discussion raises an essential question—should we trust nature to fix problems or take responsibility?",

//"A bold argument! The article challenges climate urgency, but does history provide evidence that nature can counteract human-caused disruptions?",

//"A refreshing take on environmental discourse! It suggests adaptation is inevitable, but can modern industrial damage be compared to prehistoric shifts?",
//};

//            List<string> kings_2 = new()
//{
//  "A controversial take on climate change! The article suggests nature will self-regulate, but does human intervention accelerate destruction beyond natural recovery?",

//"A bold argument against climate panic! While Earth has survived past disruptions, should we assume human activity will have no lasting impact?",

//"A smart critique of climate narratives! The article questions whether human intervention is necessary, but does adaptation always mean survival for all species?",

//"A thought-provoking discussion! It challenges whether nature will correct itself in time or if human influence has created irreversible damage",

//"A fascinating historical comparison! The dinosaurs lived for millions of years, but their extinction proves nature does not always maintain balance",

//"A compelling debate on planetary resilience! While Earth may endure, should humanity rely on nature to fix the problems we actively contribute to?",

//"A necessary discussion on environmental adaptation! The article raises an important question—does nature heal itself, or does it simply evolve beyond what existed before?",

//"A bold challenge to climate urgency! The article dismisses the idea of intervention, but does evidence support that nature alone will resolve current issues?",

//"A fresh perspective! While panic may not be productive, is ignoring climate change risks a viable strategy for long-term planetary health?",

//"A smart reflection on Earth’s resilience! The article presents an alternative viewpoint, but does adaptation guarantee survival for all ecosystems?",

//"A controversial argument! The discussion challenges the severity of climate concerns but avoids addressing the ethical responsibility of human actions",

//"A compelling counterpoint! The article suggests nature will self-correct, but does human damage push ecological recovery beyond normal cycles?",

//"A strong critique of environmental activism! While adaptation is inevitable, does human impact cause accelerated loss rather than gradual change?",

//"A thought-provoking challenge! The article forces readers to ask whether nature is truly self-sustaining or if intervention is required to limit destruction",

//"A bold discussion on planetary endurance! While extinction events have occurred naturally, do human-driven changes follow the same patterns?",

//"A fascinating look at climate adaptation! The article suggests Earth has survived past disruptions, but what if human interference speeds up instability?",

//"A necessary debate! If nature finds a way to restore balance, does that mean humanity’s survival is equally assured?",

//"A smart perspective! While nature is resilient, does that mean we should dismiss efforts to mitigate climate change?",

//"A striking discussion! The article questions climate urgency, but does human influence on ecosystems create lasting instability?",

//"A fresh take on Earth’s natural cycles! While history shows resilience, does modern industry pose a unique challenge unlike past disruptions?",

//"A thought-provoking challenge to climate concerns! The article argues that nature will adapt, but does it mean human civilization will be safe?",

//"A fascinating historical reflection! While Earth has endured mass extinctions, does it justify ignoring environmental consequences?",

//"A necessary debate on human impact! The article suggests intervention is unnecessary, but what if industrial pollution accelerates instability?",

//"A compelling take on adaptation! Nature may restore balance, but does that mean current ecosystems will remain intact?",

//"A bold critique of climate activism! While fear-based messaging exists, should we entirely dismiss environmental concerns?",

//"A sharp debate on planetary survival! If nature adapts, does that ensure human populations remain unaffected?",

//"A powerful challenge to climate panic! The article raises questions about intervention, but should governments ignore sustainability efforts?",

//"A fresh look at environmental resilience! The article asks whether nature corrects itself, but does human interference change recovery patterns?",

//"A necessary conversation! If Earth can self-regulate, should industries continue pollution unchecked?",

//"A fascinating perspective! While ecosystems are adaptable, does human-driven climate change exceed nature’s ability to recover?",
//};

//            List<string> kings_3 = new()
//{
//   "A deeply engaging argument! The article suggests nature is self-sustaining, but does human interference alter the balance beyond recovery?",

//"A bold challenge to climate activism! While fear-based messaging exists, should environmental concerns be entirely dismissed?",

//"A powerful critique of interventionist climate policies! If nature adapts, does that mean human-driven environmental protection efforts are unnecessary?",

//"A striking perspective on planetary survival! While Earth has endured past changes, does modern pollution speed up ecological instability?",

//"A compelling discussion! The article questions whether nature alone can correct the damage caused by industry and human activity",

//"A necessary debate! If nature restores balance, should society focus on adapting rather than preventing climate shifts?",

//"A fascinating take on planetary endurance! While Earth has proven resilient, does that mean human civilizations will be equally adaptive?",

//"A smart critique of environmental alarmism! While nature may find equilibrium, does that guarantee survival for all species?",

//"A thought-provoking discussion on adaptation! The article suggests nature will take care of itself, but does human intervention disrupt that process?",

//"A bold stance against climate concerns! While extinction events are natural, does human activity accelerate disruptions beyond nature’s ability to recover?",

//"A sharp analysis of environmental resilience! The article argues that climate panic may be unnecessary, but does history support this conclusion?",

//"A strong counterargument to climate urgency! The discussion questions whether nature will heal without human assistance",

//"A compelling perspective on Earth’s adaptability! The article suggests natural cycles remain intact despite human interference",

//"A smart challenge to climate activism! The discussion forces readers to reconsider whether human impact is truly irreversible",

//"A fascinating argument on planetary survival! While nature is resilient, does that justify continued industrial pollution?",

//"A bold exploration of extinction and recovery! If nature self-corrects, does human civilization have the same endurance?",

//"A well-structured critique of climate panic! The article questions whether fear-based messaging exaggerates environmental concerns",

//"A refreshing take on climate discussions! The article suggests natural adaptation will continue despite human influence",

//"A necessary debate on climate policy! If nature restores balance, should governments prioritize climate mitigation or adaptation?",

//"A smart discussion on Earth’s resilience! The article challenges the assumption that human influence permanently destabilizes nature",

//"A fascinating analysis of extinction events! The article suggests nature self-corrects, but should humans rely on that assumption?",

//"A thought-provoking challenge to mainstream climate concerns! While adaptation occurs, does that mean no intervention is needed?",

//"A strong argument on planetary survival! The article suggests nature remains balanced, but does modern environmental damage pose unique risks?",

//"A compelling discussion on environmental panic! If nature adapts, should human efforts focus on adjusting rather than preventing change?",

//"A bold critique of climate urgency! The article questions whether intervention prevents instability or delays natural cycles",

//"A fresh perspective! While extinction events occur, does human influence make modern environmental risks more severe?",

//"A necessary conversation! The article challenges whether climate panic is exaggerated or if urgent response is justified",

//"A fascinating breakdown of planetary cycles! The discussion argues that nature self-corrects, but does human interference disrupt those cycles?",

//"A smart counterargument to climate activism! While adaptation occurs, does pollution accelerate harmful consequences?",

//"A striking debate on environmental responsibility! If nature finds balance, should governments focus less on intervention?",

//};

//            List<string> kings_4 = new()
//{
//  "A bold challenge to climate alarmism! The article questions whether adaptation alone is enough to maintain ecological stability",

//"A compelling take on planetary survival! If nature always adapts, should humans worry about environmental collapse?",

//"A well-balanced discussion! The article questions whether climate panic leads to unnecessary intervention",

//"A sharp critique of environmental concerns! The discussion forces readers to reconsider whether human influence permanently disrupts nature",

//"A thought-provoking exploration of planetary resilience! While nature restores balance, does that mean human societies will survive unchanged?",

//"A necessary critique of environmental policy! If nature takes care of itself, should regulations shift toward adaptation?",

//"A bold argument on climate cycles! The article suggests fear-based messaging exaggerates concerns, but does that dismiss real risks?",

//"A fascinating analysis of extinction events! The discussion urges readers to reconsider whether human-driven environmental damage follows natural patterns",

//"A smart perspective on planetary endurance! If nature adapts, does that mean climate policies should focus on adjusting rather than preventing changes?",

//"A fresh critique of climate urgency! The article forces readers to reconsider whether global efforts to prevent climate change are necessary",

//"A compelling discussion on ecological balance! The article argues that nature self-regulates, but does human interference delay recovery?",

//"A striking argument against climate panic! While nature restores itself, should humans assume survival without intervention?",

//"A fascinating exploration of climate narratives! The discussion challenges whether concerns about human influence are exaggerated",

//"A necessary debate on environmental responsibility! Should policies shift toward managing change rather than attempting prevention?",

//"A bold take on planetary cycles! If extinction events are natural, does that mean human-driven climate shifts are inevitable?",

//"A smart analysis of adaptation! The article questions whether human influence disrupts nature or is part of its evolutionary process",

//"A powerful counterpoint to climate urgency! The discussion challenges whether intervention prevents instability or simply delays inevitable cycles",

//"A sharp critique of environmental policies! Should climate efforts shift toward resilience rather than prevention?",

//"A compelling breakdown of planetary stability! The article argues that nature is resilient, but does modern industry exceed normal adaptation limits?",

//"A fresh discussion on environmental ethics! If nature self-regulates, does that mean humans bear no responsibility for climate shifts?",

//"A bold challenge to sustainability policies! The article argues that nature restores itself, but should regulations support adaptation rather than prevention?",

//"A fascinating perspective on planetary survival! While ecosystems adapt, does human civilization have the same resilience?",

//"A smart critique of climate interventions! If nature self-corrects, do emergency climate policies create unnecessary panic?",

//"A necessary debate on environmental responsibility! The discussion forces readers to rethink whether sustainability efforts are productive",

//"A striking argument against climate urgency! The article questions whether human influence changes nature beyond repair",

//"A powerful reflection on planetary balance! While Earth has survived mass extinctions, does that justify current levels of industrial impact?",

//"A compelling discussion on environmental cycles! The article suggests adaptation is inevitable, but should humans take action?",

//"A fascinating take on climate concerns! The discussion presents arguments for trusting natural adaptation over government intervention",

//"A thought-provoking debate on sustainability! If nature self-regulates, should climate policies focus on resilience rather than control?",

//"A must-read discussion! The article questions whether climate alarmism is justified or if adaptation is inevitable",
//};

//            Threads? thread_king = await _unitOfWork.ThreadsRepository.GetThreadById(Guid.Parse("752beb3b-16cb-44d5-99d6-cd79f2b62ee4"));

//            if (thread_king is not null)
//            {
//                //create the comments
//                for (var i = 0; i < kings_2.Count; i++)
//                {

//                    var resulti = thread_king.SaveComment(
//                        kings_2[i],
//                        Guid.Parse(commentators1Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
//                        );

//                    //for the first comment in the list
//                    if (i == 0)
//                    {
//                        for (var j = 0; j < kings_1.Count; j++)
//                        {
//                            thread_king.SaveResponse(
//                                resulti.Value,
//                                kings_1[j],
//                                Guid.Parse(commentators[j]),
//                                Guid.Parse(commentators1Layer[i]),
//                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
//                        }
//                    }

//                    var resultk = thread_king.SaveResponse(
//                        resulti.Value,
//                        kings_3[i],
//                        Guid.Parse(commentators2Layer[i]),
//                         Guid.Parse(commentators1Layer[i]),
//                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

//                    var resultl = thread_king.SaveResponse(
//                        resultk.Value,
//                        kings_4[i],
//                        Guid.Parse(commentators3Layer[i]),
//                        Guid.Parse(commentators2Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

//                }

//                _unitOfWork.RepositoryFactory<Threads>().Update(thread_king);

//            }


//            //4 - overlords

//            List<string> overlords_1 = new()
//{
//    "A powerful exposé! The article forces readers to confront the harsh reality that slavery still exists despite legal prohibitions",

//"A necessary discussion! It highlights how economic and political factors contribute to the ongoing exploitation of vulnerable populations",

//"A compelling critique of global inaction! While slavery is officially banned, the article points out how loopholes allow it to persist in different forms",

//"A thought-provoking reflection on systemic inequality! The article challenges developed nations to do more in combating modern slavery",

//"A bold examination of hidden exploitation! The piece highlights how human trafficking and forced labor are still deeply entrenched in global supply chains",

//"A sharp critique of global responsibility! While progress has been made in abolishing slavery officially, systemic issues allow it to thrive unnoticed",

//"A must-read discussion! The article exposes how economic disparities create conditions where slavery continues under different names",

//"A striking revelation! It argues that modern slavery is not a relic of the past—it’s a thriving industry that governments often ignore",

//"A necessary examination of economic injustice! The discussion exposes how labor exploitation disproportionately affects underdeveloped nations",

//"A powerful call to action! The article urges stronger policies to address forced labor, human trafficking, and economic disparities that sustain modern slavery",
//};

//            List<string> overlords_2 = new()
//{
//  "A deeply important conversation! The article highlights how modern slavery thrives in industries that rely on cheap labor and human trafficking",

//"A necessary critique of global inaction! While laws exist against slavery, enforcement remains weak, allowing exploitation to continue",

//"A powerful breakdown of systemic inequality! The article questions whether developed nations are truly invested in eradicating modern slavery or if they benefit from it",

//"A fascinating examination of human rights violations! The discussion forces readers to reconsider whether current laws are enough to protect vulnerable populations",

//"A bold critique of economic exploitation! The article highlights how underdeveloped nations are disproportionately affected by forced labor and trafficking",

//"A compelling discussion on hidden injustice! While slavery is outlawed, the article challenges the idea that progress has been truly achieved",

//"A thought-provoking perspective! The article argues that modern slavery remains a profitable industry because global systems allow it to exist",

//"A sharp critique of wealth disparity! The article suggests that addressing economic inequality is necessary to eliminate modern slavery",

//"A must-read examination of labor exploitation! The discussion urges governments and corporations to acknowledge their roles in sustaining forced labor",

//"A striking call for reform! The article forces readers to confront the harsh reality that slavery never truly ended—it just evolved",

//"A deeply insightful reflection! The article reveals how forced labor is still embedded in industries that claim to uphold ethical standards",

//"A powerful discussion on corporate responsibility! It forces companies to address how supply chains contribute to modern slavery",

//"A sharp analysis of human trafficking networks! The article sheds light on how global migration and economic instability fuel exploitation",

//"A bold take on labor injustice! It examines how financial systems benefit from cheap labor while failing to implement real protections",

//"A compelling argument for stronger policies! The article urges global leaders to take more aggressive action against slavery",

//"A necessary exploration of hidden labor practices! The article examines how industries profit from forced labor under the guise of legal employment",

//"A smart breakdown of systemic failures! While international laws ban slavery, the article highlights how enforcement remains weak",

//"A striking discussion on wealth inequality! It forces readers to reconsider whether economic development efforts actually address the root of slavery",

//"A must-read perspective! The article presents a strong case for reevaluating policies that claim to fight slavery but ultimately maintain exploitation",

//"A fascinating discussion on historical and modern slavery! It argues that the mechanisms of forced labor have adapted to fit modern economies",

//"A bold critique of global supply chains! The article challenges whether corporations can truly claim ethical business practices while benefiting from forced labor",

//"A deeply important argument! It exposes how slavery thrives under economic systems designed to maximize profit at the expense of human dignity",

//"A fresh take on human rights violations! The article urges governments to enforce stronger protections against labor exploitation",

//"A powerful investigation into forced labor networks! It examines how underdeveloped regions serve as breeding grounds for slavery",

//"A compelling challenge to corporate ethics! The article questions whether industries are genuinely invested in ending slavery or simply avoiding accountability",

//"A thought-provoking discussion on underdevelopment! It highlights how economic stagnation prevents real solutions to forced labor",

//"A striking call for transparency! The article argues that many companies profit from hidden slavery despite publicly condemning it",

//"A smart examination of systemic failures! The discussion forces readers to confront how modern slavery exists within legal structures",

//"A necessary reflection on wealth disparity! The article suggests that economic empowerment is essential to eliminating slavery",

//"A must-read critique of global inaction! The discussion urges immediate reforms to dismantle forced labor networks",
//};

//            List<string> overlords_3 = new()
//{
//   "A deeply unsettling but necessary discussion! The article exposes how modern slavery remains embedded in global industries despite legal bans",

//"A sharp critique of economic exploitation! It highlights how wealth disparity fuels forced labor and trafficking worldwide",

//"A powerful call for systemic change! The article argues that developed nations must take stronger actions against hidden slavery",

//"A thought-provoking challenge to corporate ethics! It questions whether businesses are complicit in sustaining modern slavery",

//"A must-read examination of labor exploitation! The discussion urges global leaders to confront their failures in addressing forced labor",

//"A bold critique of governmental inaction! The article makes a strong case for holding policymakers accountable for failing to dismantle modern slavery",

//"A striking analysis of supply chain injustice! It forces corporations to evaluate their indirect contributions to slavery",

//"A fresh take on economic oppression! The article highlights how underdevelopment creates conditions where slavery can thrive",

//"A compelling discussion on policy failures! The article argues that slavery persists because enforcement remains weak and inconsistent",

//"A necessary conversation on human rights violations! It exposes how forced labor and trafficking continue despite international agreements",

//"A deeply insightful exploration of systemic oppression! The article questions whether global economic policies actively sustain slavery",

//"A sharp critique of wealth disparity! The discussion challenges developed nations to do more in dismantling modern slavery",

//"A powerful argument for accountability! The article forces readers to reconsider whether governments are truly invested in eradicating forced labor",

//"A thought-provoking reflection on corporate greed! It highlights how industries profit from hidden labor exploitation",

//"A bold call for reform! The article argues that without stronger regulations, slavery will continue thriving in plain sight",

//"A must-read critique of policy failure! The article presents strong reasoning for reevaluating how governments handle forced labor",

//"A striking discussion on economic disparity! It questions whether underdevelopment is deliberately maintained to fuel labor exploitation",

//"A compelling challenge to human rights enforcement! The article makes the case that slavery remains widespread due to weak governance",

//"A necessary breakdown of hidden labor networks! The discussion sheds light on how trafficking and forced labor continue unchecked",

//"A fresh examination of corporate responsibility! It argues that businesses must actively ensure their supply chains do not rely on hidden slavery",

//"A deeply unsettling but necessary critique! The article exposes how slavery has evolved into more sophisticated but equally abusive practices",

//"A thought-provoking call for stronger governance! It questions whether international laws are enough to prevent forced labor",

//"A bold discussion on economic exploitation! The article highlights how wealth disparity fuels slavery under different names",

//"A smart breakdown of systemic failure! It challenges whether governments are truly invested in eliminating modern slavery",

//"A compelling argument for greater corporate accountability! The article suggests that industries knowingly benefit from labor exploitation",

//"A necessary reflection on underdevelopment! It exposes how economic stagnation allows slavery to persist",

//"A striking examination of institutional corruption! The article challenges whether policymakers have a vested interest in maintaining forced labor",

//"A powerful argument for policy reform! It urges stronger enforcement of human rights protections",

//"A fascinating critique of global trade practices! The article exposes how slavery continues in industries that claim ethical labor standards",

//"A smart discussion on wealth disparity! It suggests that economic restructuring is key to eliminating forced labor",
//};

//            List<string> overlords_4 = new()
//{
//  "A bold critique of corporate hypocrisy! The article questions whether industries truly aim to eliminate slavery or simply avoid scrutiny",

//"A powerful reflection on systemic oppression! The discussion forces readers to reconsider whether slavery remains a foundation of global wealth",

//"A compelling argument for stronger international cooperation! The article highlights how modern slavery requires a united global effort to dismantle",

//"A sharp analysis of economic manipulation! It challenges the idea that underdevelopment is accidental rather than deliberately maintained",

//"A must-read discussion on forced labor! The article questions whether governments are serious about tackling slavery or simply ignoring its existence",

//"A thought-provoking debate on human rights violations! It examines why slavery continues despite widespread global awareness",

//"A striking critique of supply chain exploitation! The article makes the case for holding corporations accountable",

//"A fresh perspective on systemic inequality! The article exposes how forced labor benefits industries that claim ethical responsibility",

//"A bold call for transparency! It urges policymakers and corporations to stop hiding behind weak enforcement measures",

//"A necessary discussion on labor injustice! It argues that modern slavery thrives because global leaders fail to take decisive action",

//"A smart analysis of human trafficking networks! The article exposes how economic conditions fuel forced labor worldwide",

//"A powerful critique of hidden slavery! The discussion challenges whether modern industries can truly claim ethical practices",

//"A compelling argument for legislative overhaul! It urges stronger regulations to eliminate forced labor",

//"A must-read exploration of economic disparity! The article questions whether poverty is deliberately sustained to fuel exploitation",

//"A striking call for corporate accountability! The discussion presents strong arguments for investigating supply chain abuses",

//"A thought-provoking reflection on human rights failures! It forces readers to confront the reality that slavery never fully ended",

//"A bold challenge to global governance! The article argues that institutions have failed to create effective solutions against slavery",

//"A necessary critique of systemic oppression! The article suggests that economic restructuring is the only way to eliminate modern slavery",

//"A fresh take on hidden exploitation! It reveals how industries rely on forced labor despite publicly condemning slavery",

//"A powerful discussion on wealth inequality! The article questions whether economic systems are deliberately designed to maintain slavery",

//"A smart breakdown of failed policies! The article presents strong evidence that current regulations are not enough to dismantle forced labor",

//"A compelling perspective on global injustice! The article urges immediate action to combat modern slavery",

//"A bold critique of economic stagnation! It argues that underdevelopment creates conditions where slavery can flourish",

//"A must-read discussion on ethical responsibility! The article forces readers to reconsider whether society truly opposes forced labor",

//"A fascinating analysis of international trade practices! It challenges whether developed nations are doing enough to eliminate slavery",

//"A striking critique of governance failures! It argues that policymakers must prioritize human rights protections more aggressively",

//"A thought-provoking examination of labor exploitation! The article exposes how modern slavery is hidden in legal employment structures",

//"A smart debate on institutional corruption! It questions whether governments are fully committed to ending slavery or if they tolerate it for economic benefit",

//"A powerful argument for systemic change! The article suggests that only economic restructuring will truly end modern slavery",

//"A necessary conversation on labor injustice! The discussion urges industries and governments alike to confront their roles in maintaining forced labor",
//};

//            Threads? thread_overlord = await _unitOfWork.ThreadsRepository.GetThreadById(Guid.Parse("cda84695-b971-419c-978a-5c22080be969"));

//            if (thread_overlord is not null)
//            {
//                //create the comments
//                for (var i = 0; i < overlords_2.Count; i++)
//                {

//                    var resulti = thread_overlord.SaveComment(
//                        overlords_2[i],
//                        Guid.Parse(commentators1Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
//                        );

//                    //for the first comment in the list
//                    if (i == 0)
//                    {
//                        for (var j = 0; j < overlords_1.Count; j++)
//                        {
//                            thread_overlord.SaveResponse(
//                                resulti.Value,
//                                overlords_1[j],
//                                Guid.Parse(commentators[j]),
//                                Guid.Parse(commentators1Layer[i]),
//                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
//                        }
//                    }

//                    var resultk = thread_overlord.SaveResponse(
//                        resulti.Value,
//                        overlords_3[i],
//                        Guid.Parse(commentators2Layer[i]),
//                         Guid.Parse(commentators1Layer[i]),
//                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

//                    var resultl = thread_overlord.SaveResponse(
//                        resultk.Value,
//                        overlords_4[i],
//                        Guid.Parse(commentators3Layer[i]),
//                        Guid.Parse(commentators2Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

//                }

//                _unitOfWork.RepositoryFactory<Threads>().Update(thread_overlord);

//            }



//            //5 - Constitutional crises
//            List<string> constitutions_1 = new()
//{
//    "A compelling argument! The article makes a strong case that constitutional amendments can no longer keep pace with the complexities of modern governance",

//"A thought-provoking discussion! The US. Constitution has endured for over years, but the question remains: is adaptation through amendments enough, or is an overhaul necessary?",

//"A bold critique of constitutional rigidity! The article presents a powerful argument that incremental changes may not be enough to address modern challenges",

//"A necessary conversation! While tradition holds great value, governance must evolve alongside society—the article raises an important debate",

//"A fascinating take on constitutional law! The piece argues that founding principles may need modernization to reflect contemporary political and technological realities",

//"A well-argued perspective! It suggests that democracy must be adaptable, and constitutional reforms must reflect the needs of modern citizens",

//"A compelling discussion on legal evolution! The article challenges the notion that the Constitution should remain untouched despite societal advancements",

//"A sharp critique of constitutional stagnation! The piece forces readers to evaluate whether foundational laws remain effective in addressing today’s challenges",

//"A bold call for modernization! The article highlights how outdated systems can hinder governance rather than strengthen it",

//"A smart critique of constitutional amendments! It questions whether incremental changes are sufficient or if an entirely new framework is needed",
//};

//        List<string> constitutions_2 = new()
//{
//  "A powerful argument for constitutional reform! The article suggests that foundational laws should evolve alongside societal progress rather than remain static",

//"A necessary critique of outdated governance! The Constitution was created for a vastly different world, and this article presents a compelling case for modernization",

//"A fascinating legal discussion! It raises important questions about whether an amendment-based approach can keep up with contemporary challenges",

//"A bold take on constitutional reform! The article challenges readers to reconsider whether incremental updates are sufficient for modern governance",

//"A compelling call for structural change! The discussion highlights how outdated frameworks can obstruct progress rather than facilitate effective leadership",

//"A strong critique of constitutional tradition! While historical preservation is valuable, laws must be adaptable to meet contemporary needs",

//"A thought-provoking debate! The article presents a strong argument that some aspects of the Constitution may no longer serve their intended function in today’s world",

//"A refreshingly bold perspective! The piece forces readers to rethink whether governance should be guided by centuries-old laws or modern realities",

//"A necessary discussion on legal evolution! The article questions whether foundational structures can effectively respond to modern societal shifts",

//"A smart exploration of democratic adaptation! The Constitution must reflect the era it governs, and this article makes a strong case for that principle",

//"A striking argument for constitutional overhaul! The article highlights how new challenges require new governance approaches",

//"A compelling discussion! The piece questions whether incremental amendments are enough or if a fundamental rewrite is necessary",

//"A fascinating legal perspective! The article forces readers to examine whether the Constitution can adequately address today’s issues",

//"A bold call for modernization! It highlights the urgency of adapting governance structures to fit contemporary needs",

//"A well-researched discussion! The piece presents strong reasoning for why constitutional evolution is essential in an ever-changing world",

//"A powerful reflection on legal stagnation! The article explores how constitutional rigidity can hinder national progress",

//"A striking critique of outdated legal frameworks! It urges policymakers to consider broader reforms rather than relying solely on amendments",

//"A thought-provoking take on governance! The article forces readers to confront the limitations of historical legal documents in today’s political landscape",

//"A compelling critique of constitutional longevity! The discussion challenges the belief that the Constitution should remain unchanged indefinitely",

//"A necessary exploration of governance evolution! The article argues that modern challenges require modern solutions, including legal restructuring",

//"A bold and timely discussion! The article presents a strong case for adapting foundational laws to meet the demands of the st century",

//"A fascinating legal critique! It challenges the assumption that the Constitution must remain untouched despite significant societal advancements",

//"A compelling perspective on constitutional reform! The article presents a smart argument for reevaluating governance structures",

//"A necessary conversation! This discussion forces policymakers and citizens alike to reconsider whether the Constitution is still suitable for today’s world",

//"A sharp critique of constitutional stagnation! The article provides strong reasoning for why foundational laws must evolve alongside society",

//"A powerful legal argument! The discussion presents a compelling case for modernizing governance to better address current challenges",

//"A thought-provoking discussion on governance reform! The article urges readers to rethink whether historical frameworks can effectively govern modern societies",

//"A refreshingly bold perspective on constitutional reform! The piece highlights why governance structures must adapt to ensure lasting effectiveness",

//"A smart analysis of democratic evolution! The article presents a compelling argument for updating outdated constitutional provisions",

//"A striking critique of governance rigidity! The discussion highlights how legal structures must evolve rather than remain frozen in time",
//};

//        List<string> constitutions_3 = new()
//{
//   "A fresh take on constitutional reform! The article challenges the notion that historical documents must remain unchanged despite evolving governance needs",

//"A deeply insightful discussion! It questions whether legal traditions should dictate modern policies or if innovation is necessary",

//"A smart critique of outdated legal frameworks! The article urges readers to reconsider whether past governance structures can still address today’s challenges",

//"A powerful reflection on legal adaptation! The discussion presents a compelling argument for reevaluating foundational laws",

//"A bold and necessary critique! The article challenges the belief that historical precedents are always applicable in contemporary settings",

//"A fantastic discussion on constitutional evolution! It highlights how governance structures must reflect modern realities",

//"A fascinating legal exploration! The article makes a strong case for updating laws to better serve today’s citizens",

//"A compelling debate! It forces readers to confront whether incremental changes are enough to modernize an outdated Constitution",

//"A thought-provoking take on legal adaptation! The article encourages a critical discussion on whether old frameworks can meet modern needs",

//"A necessary examination of governance reform! The discussion highlights the urgency of ensuring foundational documents remain relevant",

//"A bold argument for constitutional modernization! The article presents a strong case for rethinking governance structures",

//"A well-balanced discussion! It forces policymakers to reconsider whether amendments can truly address modern governance challenges",

//"A fascinating critique of historical preservation! The article questions whether governance structures should evolve or remain rooted in tradition",

//"A necessary call for legal reevaluation! It presents strong reasoning for why governance must be adaptable rather than static",

//"A compelling perspective on national reform! The article makes a strong case for legal evolution to reflect today’s world",

//"A powerful discussion on governance flexibility! It urges readers to rethink whether foundational laws should be adjusted to fit contemporary issues",

//"A sharp critique of constitutional rigidity! The article presents a strong argument for reconsidering governance structures in the modern age",

//"A fantastic legal reflection! It highlights the tension between constitutional tradition and contemporary governance challenges",

//"A smart discussion on government adaptability! The article urges reconsideration of outdated laws in favor of modern solutions",

//"A must-read perspective! It forces readers to examine whether foundational laws should be periodically reevaluated",

//"A striking critique of governance stagnation! The article suggests that legal adaptation is necessary to ensure lasting national progress",

//"A bold challenge to constitutional tradition! The discussion encourages readers to think critically about whether historical frameworks still apply",

//"A necessary conversation! The article presents a strong case for modernizing governance structures to reflect today’s challenges",

//"A compelling discussion on legal longevity! It forces policymakers and citizens alike to reconsider whether amendments are sufficient for constitutional adaptation",

//"A thought-provoking examination of governance structures! The article challenges the idea that past legal systems remain universally applicable",

//"A fascinating dive into constitutional reform! It highlights how foundational laws must evolve alongside technological and societal advancements",

//"A smart critique of governance limitations! The article forces readers to consider whether democracy benefits from periodic constitutional reviews",

//"A bold call for modernization! The discussion presents strong reasoning for why foundational documents must reflect contemporary realities",

//"A powerful argument for legal flexibility! The article urges policymakers to address governance evolution with long-term strategies",

//"A necessary debate on governance transformation! It forces reflection on whether amendments are enough to fix systemic issues",
//};

//        List<string> constitutions_4 = new()
//{
//  "A deeply engaging discussion! The article presents a compelling argument for reconsidering constitutional governance structures",

//"A striking perspective on legal adaptation! It explores whether amendments can keep up with rapidly evolving societal challenges",

//"A bold critique of constitutional stagnation! The article makes a strong case for reforming outdated governance principles",

//"A fascinating breakdown of legal evolution! It challenges readers to rethink whether past governance strategies remain effective today",

//"A necessary discussion on structural change! The article presents a compelling case for constitutional reevaluation",

//"A powerful call for national reform! The article highlights the importance of ensuring governance structures evolve with the times",

//"A compelling perspective on constitutional flexibility! It forces readers to consider whether amendments alone can sustain long-term national development",

//"A thought-provoking discussion on governance renewal! The article urges policymakers to evaluate constitutional adaptation more comprehensively",

//"A fresh take on governance reform! The discussion raises key questions about whether traditional frameworks still meet modern needs",

//"A well-researched perspective! The article provides strong reasoning for why foundational laws must remain adaptable",

//"A compelling breakdown of legal evolution! The discussion encourages readers to think critically about constitutional longevity",

//"A bold challenge to governance stagnation! It argues that national stability requires flexible constitutional frameworks",

//"A fascinating discussion on legal transformation! The article urges policymakers to focus on governance longevity rather than outdated preservation",

//"A smart critique of constitutional rigidity! The discussion presents strong reasoning for reevaluating governance structures",

//"A necessary call for national modernization! The article forces readers to reconsider whether past legal frameworks remain viable today",

//"A striking argument for constitutional restructuring! The discussion presents a compelling case for updating governance strategies",

//"A well-balanced debate on governance transformation! The article questions whether past legal principles are still effective",

//"A fascinating exploration of legal reform! It argues for governance flexibility to reflect modern realities",

//"A compelling call for constitutional modernization! The article urges policymakers to evaluate governance structures more critically",

//"A bold discussion on democratic reform! It challenges readers to rethink whether amendments alone can ensure national progress",

//"A smart debate on governance adaptability! The discussion presents key questions on constitutional flexibility",

//"A necessary conversation on legal evolution! The article encourages thoughtful analysis of governance adaptation",

//"A well-researched examination of constitutional longevity! It challenges traditional perspectives on governance reform",

//"A powerful critique of governance stagnation! The discussion raises key points about legal flexibility in national development",

//"A striking breakdown of constitutional adaptation! The article presents a strong case for reevaluating foundational governance structures",

//"A fresh perspective on governance renewal! It questions whether past constitutional principles can sustain future national stability",

//"A fascinating discussion on governance flexibility! The article highlights the importance of legal adaptation in a rapidly changing world",

//"A bold argument for modernizing governance structures! The discussion raises critical points about constitutional longevity",

//"A thought-provoking debate on constitutional restructuring! The article urges policymakers to assess governance evolution more comprehensively",

//"A necessary reflection on legal transformation! The article presents a strong case for evaluating governance sustainability",
//};

//        Threads? thread_constitution = await _unitOfWork.ThreadsRepository.GetThreadById(Guid.Parse("7b8be749-262b-4ac6-b135-5fe8130ede42"));

//        if (thread_constitution is not null)
//            {
//                //create the comments
//                for (var i = 0; i < constitutions_2.Count; i++)
//                {

//                    var resulti = thread_constitution.SaveComment(
//                        constitutions_2[i],
//                        Guid.Parse(commentators1Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
//                        );

//                    //for the first comment in the list
//                    if (i == 0)
//                    {
//                        for (var j = 0; j < constitutions_1.Count; j++)
//                        {
//                            thread_constitution.SaveResponse(
//                                resulti.Value,
//                                constitutions_1[j],
//                                Guid.Parse(commentators[j]),
//                                Guid.Parse(commentators1Layer[i]),
//                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
//                        }
//                    }

//                    var resultk = thread_constitution.SaveResponse(
//                        resulti.Value,
//                        constitutions_3[i],
//                        Guid.Parse(commentators2Layer[i]),
//                         Guid.Parse(commentators1Layer[i]),
//                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

//                    var resultl = thread_constitution.SaveResponse(
//                        resultk.Value,
//                        constitutions_4[i],
//                        Guid.Parse(commentators3Layer[i]),
//                        Guid.Parse(commentators2Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

//                }

//                _unitOfWork.RepositoryFactory<Threads>().Update(thread_constitution);

//            }



//            //6 - Trump derangement syndrome

//            List<string> trumps_1 = new()
//{
//    "A fascinating exploration of media and public perception! The article highlights how Trump’s influence extends beyond politics into culture and entertainment",

//"A compelling discussion on political legacy! It presents strong reasoning for why Trump remains a central figure long after his presidency",

//"A thought-provoking analysis! While some leaders fade into obscurity after leaving office, Trump’s presence continues to dominate headlines",

//"A smart critique of media obsession! The article suggests that coverage of Trump is driven by audience engagement rather than objective analysis",

//"A striking discussion on political polarization! Whether admired or criticized, the article argues that Trump remains impossible to ignore",

//"A bold take on leadership perception! The discussion forces readers to reconsider whether Trump’s impact is exaggerated or genuinely historic",

//"A necessary exploration of political branding! The article suggests that Trump’s ability to maintain public interest is unparalleled",

//"A compelling argument for political curiosity! Whether viewed positively or negatively, the discussion acknowledges Trump’s lasting relevance",

//"A fresh perspective on political fascination! The article highlights how Trump has remained a cultural force even beyond policy decisions",

//"A must-read discussion! It explores why Trump’s persona, business background, and leadership style continue to command attention",
//};

//            List<string> trumps_2 = new()
//{
//  "A deeply insightful discussion on political legacy! The article forces readers to reconsider why Trump remains a polarizing figure",

//"A smart critique of media engagement! It argues that attention on Trump is fueled by both admiration and controversy",

//"A bold exploration of leadership style! The article highlights how Trump’s approach continues to spark debate even after his presidency",

//"A compelling breakdown of public fascination! Whether viewed as influential or controversial, Trump remains a dominant figure in US. politics",

//"A fresh take on political branding! The article suggests that Trump’s ability to command attention is a strategic success",

//"A must-read discussion! It questions why Trump receives more scrutiny than past presidents with arguably worse track records",

//"A striking examination of media narratives! It forces reflection on whether public perception is shaped by reporting rather than leadership outcomes",

//"A fascinating perspective on historical relevance! The article argues that Trump’s presidency is unique due to its lasting influence",

//"A necessary conversation on political influence! The article presents strong reasoning for Trump’s ongoing relevance",

//"A thought-provoking challenge to leadership comparisons! It forces readers to reconsider what truly defines presidential success",

//"A powerful critique of media fixation! The article suggests that Trump remains a news cycle priority due to audience engagement",

//"A compelling take on modern leadership! The discussion urges readers to assess how Trump’s presence continues shaping political discourse",

//"A fresh analysis of political branding! The article suggests Trump’s ability to attract attention is unmatched",

//"A smart breakdown of media polarization! The discussion explores how Trump’s actions generate extreme reactions",

//"A bold take on legacy politics! The article questions whether Trump’s impact is exaggerated or genuinely historic",

//"A must-read critique of leadership perception! It challenges assumptions about presidential effectiveness and media focus",

//"A fascinating exploration of political identity! The article highlights how Trump reshaped leadership expectations",

//"A powerful reflection on historical comparisons! The discussion examines whether Trump deserves the level of obsession he receives",

//"A striking argument on leadership scrutiny! The article presents compelling reasons why Trump’s presidency attracts so much attention",

//"A necessary examination of Trump’s cultural presence! The discussion forces readers to consider how political figures influence society beyond policy",

//"A sharp take on presidential branding! The article challenges traditional notions of political influence",

//"A compelling breakdown of media coverage! It examines why Trump continues to dominate political discussions",

//"A fascinating analysis of post-presidency engagement! The article suggests Trump’s relevance extends far beyond his time in office",

//"A bold discussion on political identity! The article presents strong reasoning for why Trump remains an unavoidable figure",

//"A must-read reflection on public discourse! It urges readers to assess whether media attention exaggerates or fairly represents Trump’s influence",

//"A smart argument on the evolution of political attention! The discussion examines whether Trump’s presence is part of a broader media shift",

//"A striking critique of political analysis! The article questions whether Trump’s legacy is accurately portrayed or shaped by narratives",

//"A compelling perspective on leadership fascination! It highlights why Trump remains widely discussed despite his presidency ending",

//"A fresh take on political engagement! The article questions whether media reliance on controversy fuels the obsession with Trump",

//"A necessary discussion on modern presidential scrutiny! It forces reflection on whether Trump’s treatment differs significantly from past presidents",
//};

//            List<string> trumps_3 = new()
//{
//   "A bold analysis of Trump’s continued relevance! The article challenges why media and public discourse remain fixated on him",

//"A compelling discussion on political branding! Trump’s ability to command attention is unlike any other modern politician",

//"A thought-provoking examination of legacy politics! The article forces reflection on whether Trump’s impact is exaggerated or historically significant",

//"A sharp critique of media dynamics! It questions whether coverage of Trump is driven by controversy rather than policy achievements",

//"A fresh perspective on political influence! The article argues that Trump remains unavoidable due to his leadership style and personal brand",

//"A fascinating take on public obsession! The discussion presents strong reasoning for why Trump still dominates global conversations",

//"A must-read exploration of media fixation! It forces readers to consider whether Trump’s actions receive disproportionate scrutiny",

//"A powerful breakdown of leadership perception! It questions whether Trump’s presidency was as controversial as media narratives suggest",

//"A striking argument on political longevity! The article highlights how Trump continues shaping discourse long after leaving office",

//"A necessary discussion on political fascination! It urges readers to evaluate whether Trump’s influence is based on merit or media portrayal",

//"A compelling breakdown of Trump’s lasting appeal! The discussion explores why his presence in politics has outlived his presidency",

//"A smart analysis of leadership branding! The article suggests that Trump revolutionized political communication",

//"A bold critique of media engagement strategies! It challenges whether sensationalism has played a role in maintaining public fascination",

//"A must-read reflection on historical comparisons! The article questions whether Trump’s influence is unique or part of a larger political trend",

//"A thought-provoking argument on legacy perception! It examines whether Trump deserves the level of focus he continues to receive",

//"A powerful discussion on political identity! The article suggests that Trump represents a movement beyond traditional leadership",

//"A fresh perspective on modern presidency! It forces readers to assess how Trump’s unconventional approach reshaped political expectations",

//"A striking critique of media-driven narratives! The discussion examines whether public interest in Trump is organic or manufactured",

//"A necessary conversation on political impact! The article urges readers to consider whether Trump’s influence extends beyond his policies",

//"A fascinating analysis of post-presidency engagement! It questions why Trump remains relevant while other former presidents fade from discussions",

//"A sharp breakdown of public interest! The article explores whether Trump’s influence is driven by his achievements or his ability to provoke reactions",

//"A compelling debate on political endurance! It examines why Trump remains central to conversations despite leaving office",

//"A thought-provoking challenge to leadership perception! It forces reflection on how Trump’s presidency compares to historical figures",

//"A bold exploration of personal branding! The article argues that Trump’s influence extends beyond governance into cultural discourse",

//"A must-read reflection on modern political attention! It questions whether Trump’s prominence is a result of his own efforts or media amplification",

//"A fresh critique of legacy politics! The article suggests that Trump’s presidency reshaped how leaders remain in the public eye",

//"A fascinating take on historical comparisons! It challenges whether Trump’s impact will be seen as transformative or divisive in years to come",

//"A necessary breakdown of political fascination! The article forces readers to reconsider whether public attention is justified or excessive",

//"A striking argument on Trump’s enduring influence! It explores whether his leadership created a lasting movement beyond his presidency",

//"A smart discussion on media cycles! The article suggests that Trump’s ability to stay relevant is partially due to news coverage strategies",
//};

//            List<string> trumps_4 = new()
//{
//  "A compelling examination of post-presidency influence! The discussion questions whether Trump’s impact is lasting or temporary",

//"A thought-provoking critique of leadership obsession! The article forces readers to assess why Trump dominates conversations more than past presidents",

//"A powerful take on political branding! It explores how Trump’s communication style contributed to his long-term relevance",

//"A sharp analysis of Trump’s cultural presence! The article highlights his influence beyond policy decisions",

//"A must-read discussion on modern political engagement! It questions whether Trump’s leadership shaped future expectations for politicians",

//"A fascinating breakdown of presidential comparisons! The article forces reflection on whether Trump deserves the level of scrutiny he receives",

//"A bold argument on media fixation! It suggests that Trump remains a key figure because controversy drives viewership",

//"A fresh critique of historical relevance! The article questions whether Trump’s presidency will be remembered more for policy or persona",

//"A striking analysis of legacy branding! It challenges whether Trump’s lasting impact is a product of political decisions or public fascination",

//"A necessary conversation on Trump’s influence! It urges readers to reflect on whether his relevance comes from genuine achievements or media framing",

//"A compelling reflection on political endurance! The article explores why Trump remains central to discussions long after leaving office",

//"A thought-provoking take on leadership perception! It questions whether Trump’s presidency fundamentally changed US. politics",

//"A powerful critique of media engagement! The article highlights how public interest in Trump influences news coverage",

//"A sharp breakdown of Trump’s continued dominance! It forces readers to reconsider whether political fascination with him is warranted",

//"A must-read discussion on political influence! The article examines whether Trump’s impact is sustainable or dependent on controversy",

//"A fascinating exploration of leadership legacy! It explores why some figures maintain relevance beyond their tenure",

//"A bold argument on Trump’s long-term significance! The discussion urges reflection on whether history will view his presidency as transformational",

//"A fresh take on media scrutiny! The article suggests that Trump remains central to discussions due to his ability to drive engagement",

//"A striking critique of leadership portrayal! It forces readers to rethink whether Trump’s prominence is based on merit or polarizing rhetoric",

//"A necessary analysis of public interest! The article highlights how Trump’s appeal is both deeply admired and fiercely debated",

//"A smart examination of political storytelling! It challenges whether Trump’s presidency is framed fairly in historical narratives",

//"A compelling take on modern political movements! The article explores how Trump’s leadership created lasting shifts in governance",

//"A thought-provoking discussion on legacy perception! It questions whether Trump will be remembered for policies or public fascination",

//"A powerful breakdown of media attention! The article challenges whether Trump receives disproportionate focus compared to other leaders",

//"A striking argument on leadership endurance! It examines why Trump maintains relevance unlike many former presidents",

//"A fresh perspective on historical comparisons! The discussion urges reflection on whether Trump’s prominence is justified",

//"A necessary critique of obsession with Trump! The article highlights how his presence dominates discourse more than policy merits alone",

//"A bold challenge to political narratives! The discussion suggests Trump’s leadership is both deeply polarizing and widely influential",

//"A fascinating take on the evolution of political attention! The article explores whether future leaders will command similar fascination",

//"A must-read discussion on leadership fixation! The article urges reflection on why Trump remains central to US. and global conversations",
//};

//            Threads? thread_trump = await _unitOfWork.ThreadsRepository.GetThreadById(Guid.Parse("b2601115-d323-4e92-8040-dcd0409dc511"));

//            if (thread_trump is not null)
//            {
//                //create the comments
//                for (var i = 0; i < trumps_2.Count; i++)
//                {

//                    var resulti = thread_trump.SaveComment(
//                        trumps_2[i],
//                        Guid.Parse(commentators1Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
//                        );

//                    //for the first comment in the list
//                    if (i == 0)
//                    {
//                        for (var j = 0; j < trumps_1.Count; j++)
//                        {
//                            thread_trump.SaveResponse(
//                                resulti.Value,
//                                trumps_1[j],
//                                Guid.Parse(commentators[j]),
//                                Guid.Parse(commentators1Layer[i]),
//                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
//                        }
//                    }

//                    var resultk = thread_trump.SaveResponse(
//                        resulti.Value,
//                        trumps_3[i],
//                        Guid.Parse(commentators2Layer[i]),
//                         Guid.Parse(commentators1Layer[i]),
//                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

//                    var resultl = thread_trump.SaveResponse(
//                        resultk.Value,
//                        trumps_4[i],
//                        Guid.Parse(commentators3Layer[i]),
//                        Guid.Parse(commentators2Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

//                }

//                _unitOfWork.RepositoryFactory<Threads>().Update(thread_trump);

//            }


//            //7 - ugly duckling

//            List<string> ducklings_1 = new()
//{
//    "A beautifully written piece! It reminds readers that true beauty is rooted in uniqueness, not in conforming to narrow standards",

//"A necessary conversation! This article challenges the outdated idea that beauty is tied to skin color and promotes self-love",

//"A refreshing perspective! Black beauty has always existed, and historical figures like Cleopatra and Nefertiti prove that it transcends modern biases",

//"A deeply insightful article! It urges black women to celebrate their natural features rather than conforming to societal pressures",

//"A must-read discussion! The article highlights how beauty is diverse and should never be reduced to skin tone alone",

//"A powerful critique of color-based beauty standards! This piece encourages women to embrace their heritage and individuality",

//"A compelling argument for redefining beauty! The historical references prove that brown-skinned women have always been celebrated for their elegance",

//"A timely and empowering message! Self-acceptance is far more valuable than chasing artificial beauty standards",

//"A brilliant article! It challenges stereotypes and urges black women to recognize their natural beauty",

//"A striking discussion! The article forces readers to rethink how beauty has been defined and manipulated throughout history",
//};

//            List<string> ducklings_2 = new()
//{
//  "A bold and empowering article! It reminds black women that their beauty is already celebrated in history—there’s no need to chase artificial standards",

//"A necessary discussion! The article highlights how media and society have long influenced perceptions of beauty in ways that don’t always reflect reality",

//"A compelling reminder! Cleopatra and Nefertiti were admired for their beauty, proving that elegance and attraction have never been tied to pale skin",

//"The message of self-acceptance in this article is invaluable! It urges black women to reject harmful beauty practices and embrace their natural glow",

//"A deeply thought-provoking piece! It challenges the idea that beauty can be defined by a singular standard and instead promotes diversity",

//"A fantastic critique of historical biases! Beauty exists beyond skin color, and this article makes that argument powerfully",

//"A refreshing take on self-love! It encourages black women to recognize the elegance in their natural features rather than altering their appearance",

//"A well-crafted discussion! It highlights how black beauty is not a trend but a long-standing truth that has always existed",

//"A powerful article! It tackles an important topic—beauty is not about skin color but about confidence, uniqueness, and authenticity",

//"The conversation around bleaching is a necessary one! This article presents a compelling case for rejecting artificial standards in favor of self - acceptance",

//"A fantastic reflection on history! The article reminds readers that black beauty has been recognized for centuries, far beyond modern beauty standards",

//"A strong statement against harmful beauty trends! This piece encourages readers to prioritize self-love and natural elegance",

//"A must-read discussion! It challenges harmful narratives and promotes black beauty beyond societal expectations",

//"A refreshing exploration of aesthetics! The article argues that beauty should never be dictated by external pressures but embraced in its natural form",

//"A brilliant message! It urges black women to redefine beauty by embracing their unique features instead of conforming to unrealistic ideals",

//"A striking critique of beauty standards! The article forces readers to rethink the perception of attractiveness beyond Eurocentric ideals",

//"A powerful argument for cultural appreciation! It highlights how beauty has always existed in diverse forms throughout history",

//"A compelling take on self-worth! The article promotes confidence and authenticity over artificial alterations",

//"A necessary discussion in today’s world! It forces us to reconsider what beauty truly means and who gets to define it",
//                "A beautifully written piece! It celebrates natural beauty and urges black women to take pride in their unique features",

//"A bold challenge to societal norms! The article encourages black women to reject harmful narratives and embrace their own definition of beauty",

//"A deeply insightful article! It presents historical evidence that beauty is not tied to skin color but has always been celebrated in various forms",

//"A refreshing reminder! Beauty should never be a political statement—it should be appreciated in its purest form",

//"A necessary critique of bleaching culture! The article makes a strong case for self-acceptance and natural elegance",

//"A powerful discussion! It reminds readers that attraction has always been diverse, and black beauty deserves to be celebrated on its own terms",

//"A compelling read! It forces society to reevaluate the perception of beauty beyond narrow historical biases",

//"A must-read reflection! The article urges black women to embrace their natural features without feeling pressured to conform",

//"A fascinating take on historical beauty! The examples of Cleopatra and Nefertiti prove that black beauty has been admired for centuries",

//"A bold and inspiring article! It challenges the idea that attractiveness is tied to skin color and instead promotes diversity",

//"A necessary discussion on beauty perception! The article encourages black women to reject artificial trends and appreciate their authentic selves",
//};

//            List<string> ducklings_3 = new()
//{
//   "A deeply insightful take on beauty! The article reminds black women that their elegance and attractiveness have long been celebrated",

//"A fresh and empowering discussion! It dismantles the idea that beauty should be tied to Eurocentric ideals",

//"A fantastic critique of bleaching culture! The article encourages self-love and confidence rather than artificial modifications",

//"A bold challenge to traditional beauty standards! It urges readers to embrace natural beauty rather than conforming to societal pressures",

//"A thought-provoking read! It highlights how black beauty transcends superficial standards and should be embraced for its uniqueness",

//"A striking argument for self-acceptance! The article reminds readers that beauty is not determined by skin color but by confidence and individuality",

//"A refreshing look at beauty beyond conventional definitions! The article makes a strong case for celebrating diversity",

//"A smart critique of social beauty expectations! It forces readers to reflect on whether attraction is defined by external validation or inner confidence",

//"A powerful conversation! It challenges the outdated notion that pale skin equals beauty and calls for recognition of diverse aesthetics",

//"A fantastic historical perspective! The article shows that beauty has always been diverse and should not be limited to modern trends",

//"An insightful discussion! The article promotes the idea that beauty has existed in all forms throughout history, long before today’s narratives",

//"A beautifully written argument for self-love! It reminds black women that their features deserve celebration, not alteration",

//"A bold critique of beauty biases! The article forces us to reconsider how media and society shape attractiveness",

//"A refreshing take on black beauty! It dismantles stereotypes and encourages authenticity over artificial beauty enhancements",

//"A compelling perspective! It proves that attraction and beauty are not limited to a single aesthetic—diversity is key",

//"A necessary reflection on self-acceptance! The article urges readers to focus on embracing their features rather than changing them",

//"A fascinating dive into historical perceptions of beauty! The discussion on Cleopatra and Nefertiti highlights how beauty standards evolve",

//"A smart critique of media representation! The article calls for celebrating black beauty in its natural form rather than forcing it into existing trends",

//"A powerful call for embracing uniqueness! It questions why society continues promoting narrow beauty standards",

//"A thought-provoking piece! The article forces readers to reflect on whether beauty should be dictated by external opinions or personal confidence",

//"A bold and necessary discussion! The article presents a strong case for rejecting bleaching culture in favor of self-love",

//"A compelling breakdown of beauty standards! It dismantles the idea that attraction can only exist within predefined norms",

//"A refreshing look at diversity! The article highlights that black beauty has existed long before modern narratives",

//"A fascinating critique of beauty politics! The discussion around historical beauty figures proves representation matters",

//"A deeply engaging read! It reminds black women that their attractiveness is not determined by comparison but by confidence",

//"A smart commentary on evolving beauty ideals! The article argues for appreciation rather than forced modification",

//"A strong critique of outdated biases! It questions the need for artificial beauty enhancement when authenticity is already stunning",

//"A necessary exploration of aesthetics! The article promotes the idea that beauty is about individuality rather than forced conformity",

//"A powerful message! It encourages black women to stop seeking external validation and recognize their inherent beauty",

//"A well-balanced discussion! The article presents a nuanced take on attraction beyond rigid standards",
//};

//            List<string> ducklings_4 = new()
//{
//  "A striking piece! The article reminds readers that natural beauty exists beyond societal expectations",

//"A sharp critique of media-driven beauty trends! It forces us to reflect on whether attraction should be dictated by commercial influence",

//"A beautifully structured argument! It dismantles harmful beauty standards while promoting self-confidence",

//"A fresh perspective on aesthetics! The article highlights how attraction is shaped by heritage, culture, and personal perception",

//"A powerful historical take on beauty! Cleopatra and Nefertiti prove that black beauty has always been recognized",

//"A smart discussion on social beauty expectations! The article forces us to rethink whether attraction should be evaluated through external validation",

//"A bold challenge to bleaching culture! The article promotes authenticity over artificial enhancement",

//"A fascinating take on aesthetics! It argues that true beauty lies in individuality, not in modification",

//"A refreshing discussion! The article encourages self-acceptance as the key to true beauty",

//"A must-read reflection on evolving beauty ideals! It reminds readers that attractiveness transcends skin color",

//"A powerful conversation on diversity! It highlights how beauty has always existed beyond societal definitions",

//"A fantastic breakdown of beauty perceptions! The article questions whether attraction should be dictated by external standards",

//"A compelling argument for embracing natural beauty! It challenges media-driven expectations",

//"A well-balanced discussion! The article highlights how perception influences attractiveness beyond physical features",

//"A necessary critique of bleaching culture! It promotes confidence and individuality over artificial modification",

//"A deep exploration of historical beauty! The article proves attraction has always been diverse and celebrated",

//"A smart commentary on representation! The discussion on Cleopatra and Nefertiti challenges the idea that beauty recognition is a recent concept",

//"A powerful statement on self-love! The article urges black women to recognize their unique elegance rather than conform to external pressures",

//"A bold critique of societal beauty expectations! It forces readers to reconsider how attraction is shaped",

//"A fascinating historical take! The article highlights that black beauty existed long before modern narratives",

//"A strong argument for cultural appreciation! The discussion around historical beauty figures challenges biased perceptions",

//"A fresh look at aesthetics! The article encourages authenticity rather than artificial beauty enhancement",

//"A thought-provoking reflection! It forces us to rethink whether attraction should be dictated by external opinions or personal confidence",

//"A necessary conversation! The article dismantles harmful beauty myths while promoting individuality",

//"A bold and insightful critique! It questions why artificial beauty enhancement is prioritized over embracing natural features",

//"A deeply engaging read! The article reminds black women that their elegance and attractiveness deserve recognition",

//"A fantastic exploration of evolving beauty ideals! The article argues that attraction transcends superficial standards",

//"A well-structured discussion! It presents a nuanced take on aesthetics beyond narrow definitions",

//"A striking perspective on self-confidence! The article encourages black women to embrace their beauty without feeling pressured to conform",

//"A beautifully written reflection! The article forces us to reconsider how media and culture shape attraction",
//};

//            Threads? thread_duckling = await _unitOfWork.ThreadsRepository.GetThreadById(Guid.Parse("5ec69153-2579-460b-b28f-bc976947ca21"));

//            if (thread_duckling is not null)
//            {
//                //create the comments
//                for (var i = 0; i < ducklings_2.Count; i++)
//                {

//                    var resulti = thread_duckling.SaveComment(
//                        ducklings_2[i],
//                        Guid.Parse(commentators1Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
//                        );

//                    //for the first comment in the list
//                    if (i == 0)
//                    {
//                        for (var j = 0; j < ducklings_1.Count; j++)
//                        {
//                            thread_duckling.SaveResponse(
//                                resulti.Value,
//                                ducklings_1[j],
//                                Guid.Parse(commentators[j]),
//                                Guid.Parse(commentators1Layer[i]),
//                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
//                        }
//                    }

//                    var resultk = thread_duckling.SaveResponse(
//                        resulti.Value,
//                        ducklings_3[i],
//                        Guid.Parse(commentators2Layer[i]),
//                         Guid.Parse(commentators1Layer[i]),
//                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

//                    var resultl = thread_duckling.SaveResponse(
//                        resultk.Value,
//                        ducklings_4[i],
//                        Guid.Parse(commentators3Layer[i]),
//                        Guid.Parse(commentators2Layer[i]),
//                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

//                }

//                _unitOfWork.RepositoryFactory<Threads>().Update(thread_duckling);

//            }



//        }






    }

}
