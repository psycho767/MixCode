navigator.mediaDevices.getUserMedia({ video: true })
  .then((stream) => {
      const video = document.getElementById('cameraFeed');
video.srcObject = stream;
})
  .catch((error) => {
      console.error('Error accessing camera:', error);
});

function captureAndSend() {
  const video = document.getElementById('cameraFeed');
  const canvas = document.createElement('canvas');
  const ctx = canvas.getContext('2d');
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;
    ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

    // Convert the canvas content to a data URL
  const dataUrl = canvas.toDataURL('image/png');

    // Send the image data to the server
    $.ajax({
        type: 'POST',
        url: 'CaptureImages.aspx',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ imageData: dataUrl }),
        success: function (data) {
            console.log('Image saved:', data);
        },
        error: function (error) {
            console.error('Error saving image:', error);
        }
    });
}

// Automatically capture and send an image every 5 seconds
setInterval(captureAndSend, 5000);
