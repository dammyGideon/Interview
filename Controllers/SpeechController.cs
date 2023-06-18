using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Speech.Synthesis;



namespace interview.Controllers;

[Route("api/speech")]
[ApiController]

public class SpeechController : ControllerBase  {
    
    public SpeechController(){

    }

    [HttpGet]
    public IActionResult getAll(){
        return Ok();
    }

    [HttpPost]
    public IActionResult insertText([FromBody] string text){
      using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
    {
        // Set the voice to use (optional)
        synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen);

        // Create a MemoryStream to store the synthesized speech
        MemoryStream stream = new MemoryStream();

        // Synthesize speech from the provided text
        synthesizer.SetOutputToWaveStream(stream);
        synthesizer.Speak(text);

        // Reset the stream position before returning the response
        stream.Position = 0;

        return new FileStreamResult(stream, "audio/wav")
        {
            FileDownloadName = "speech.wav"
        };
    }
    }

}