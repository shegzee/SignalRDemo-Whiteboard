
document.addEventListener("DOMContentLoaded", function () {
	const canvas = document.getElementById("whiteboard");
	const context = canvas.getContext("2d");

	// connect to SignalR hub
	const connection = new signalR.HubConnectionBuilder()
		.withUrl("/whiteboardHub")
		.configureLogging(signalR.LogLevel.Information)
		.build();

	connection.start()
		.then(() => console.log("Connected to Hub"))
		.catch(err => console.error(err.toString()));

	let isDrawing = false;
	canvas.addEventListener("mousedown", startDrawing);
	canvas.addEventListener("mousemove", mouseDraw);
	canvas.addEventListener("mouseup", stopDrawing);
	canvas.addEventListener("mouseout", stopDrawing);

	connection.on("ReceiveDraw", data => {
		// const event = new MouseEvent("mousedown", { clientX: data.x, clientY: data.y });
		remoteDraw(data);
	});

	function startDrawing(event) {
		isDrawing = true;
		mouseDraw(event);
	}

	function remoteDraw(data) {
		draw(data.x, data.y);
	}

	function mouseDraw(event) {
		if (!isDrawing) return;

		// get relative x and y coordinates for canvas
		const x = event.clientX - canvas.getBoundingClientRect().left;
		const y = event.clientY - canvas.getBoundingClientRect().top;

		draw(x, y);

		// send coordinates to SignalR
		connection.invoke("SendDraw", { x, y })
			.catch(err => console.error(err.toString()));
	}

	function draw(x, y) {
		// drawwwwwwwwwww
		context.beginPath();
		context.arc(x, y, 2, 0, 2 * Math.PI);
		context.fillStyle = "#000";
		context.fill();
		context.stroke();
		context.closePath();
	}

	function stopDrawing() {
		isDrawing = false;
	}
})