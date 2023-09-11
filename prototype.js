// Create a canvas element
var canvas = document.createElement("canvas");
canvas.width = 800;
canvas.height = 600;
document.body.appendChild(canvas);

// Get the canvas context
var ctx = canvas.getContext("2d");

// Define some parameters
var numLevels = 10; // The number of levels in the path
var levelSize = 50; // The size of each level icon
var levelGap = 20; // The gap between each level icon
var pathWidth = 10; // The width of the path line
var pathColor = "#FFC107"; // The color of the path line
var levelColors = ["#F44336", "#E91E63", "#9C27B0", "#673AB7", "#3F51B5", "#2196F3", "#03A9F4", "#00BCD4", "#009688", "#4CAF50"]; // The colors of each level icon
var levelShapes = ["circle", "square", "triangle", "pentagon", "hexagon", "star", "heart", "diamond", "cross", "moon"]; // The shapes of each level icon
var levelIcons = ["🐶", "🐱", "🐭", "🐹", "🐰", "🦊", "🐻", "🐼", "🐨", "🐯"]; // The icons of each level icon

// Draw the path line
ctx.beginPath();
ctx.moveTo(100, 100); // Move to the starting point
ctx.lineTo(100, 500); // Draw a vertical line to the bottom
ctx.lineTo(700, 500); // Draw a horizontal line to the right
ctx.lineWidth = pathWidth;
ctx.strokeStyle = pathColor;
ctx.stroke();

// Draw the level icons
for (var i = 0; i < numLevels; i++) {
  // Calculate the position of each icon
  var x, y;
  if (i < 5) {
    // The first five icons are on the left side of the path
    x = 100 - levelSize / 2;
    y = 500 - (i + 1) * (levelSize + levelGap);
  } else {
    // The last five icons are on the right side of the path
    x = 700 + levelSize / 2;
    y = 500 - (i - 4) * (levelSize + levelGap);
  }

  // Draw the icon shape using SVG path commands
  ctx.beginPath();
  switch (levelShapes[i]) {
    case "circle":
      ctx.arc(x, y, levelSize / 2, 0, Math.PI * 2); // Draw a circle
      break;
    case "square":
      ctx.rect(x - levelSize / 2, y - levelSize / 2, levelSize, levelSize); // Draw a square
      break;
    case "triangle":
      ctx.moveTo(x, y - levelSize / 2); // Move to the top point
      ctx.lineTo(x + levelSize / 2, y + levelSize / 4); // Draw a line to the bottom right point
      ctx.lineTo(x - levelSize / 2, y + levelSize / 4); // Draw a line to the bottom left point
      ctx.closePath(); // Close the path
      break;
    case "pentagon":
      ctx.moveTo(x, y - levelSize / 2); // Move to the top point
      ctx.lineTo(x + levelSize / 2, y - levelSize / 6); // Draw a line to the top right point
      ctx.lineTo(x + levelSize / 3, y + levelSize / 3); // Draw a line to the bottom right point
      ctx.lineTo(x - levelSize / 3, y + levelSize / 3); // Draw a line to the bottom left point
      ctx.lineTo(x - levelSize / 2, y - levelSize / 6); // Draw a line to the top left point
      ctx.closePath(); // Close the path
      break;
    case "hexagon":
      ctx.moveTo(x, y - levelSize / 2); // Move to the top point
      ctx.lineTo(x + levelSize / 4, y - levelSize / 2); // Draw a line to the top right point
      ctx.lineTo(x + levelSize / 2, y); // Draw a line to the right point
      ctx.lineTo(x + levelSize / 4, y + levelSize / 2); // Draw a line to the bottom right point
      ctx.lineTo(x - levelSize / 4, y + levelSize / 2); // Draw a line to the bottom left point
      ctx.lineTo(x - levelSize / 2, y); // Draw a line to the left point
      ctx.closePath(); // Close the path
      break;
    case "star":
      ctx.moveTo(x, y - levelSize / 2); // Move to the top point
      ctx.lineTo(x + levelSize / 10, y - levelSize / 10); // Draw a line to the top right inner point
      ctx.lineTo(x + levelSize / 2, y - levelSize / 10); // Draw a line to the right outer point
      ctx.lineTo(x + levelSize / 5, y + levelSize / 10); // Draw a line to the right inner point
      ctx.lineTo(x + levelSize / 3, y + levelSize / 2); // Draw a line to the bottom right outer point
      ctx.lineTo(x, y + levelSize / 4); // Draw a line to the bottom inner point
      ctx.lineTo(x - levelSize / 3, y + levelSize / 2); // Draw a line to the bottom left outer point
      ctx.lineTo(x - levelSize / 5, y + levelSize / 10); // Draw a line to the left inner point
      ctx.lineTo(x - levelSize / 2, y - levelSize / 10); // Draw a line to the left outer point
      ctx.lineTo(x - levelSize / 10, y - levelSize / 10); // Draw a line to the top left inner point
      ctx.closePath(); // Close the path
      break;
    case "heart":
      ctx.moveTo(x, y - levelSize / 6); // Move to the top middle point
      ctx.bezierCurveTo(x + levelSize / 6, y - levelSize / 2, x + levelSize / 2, y - levelSize / 3, x + levelSize / 2, y); // Draw a bezier curve to the right bottom point
      ctx.bezierCurveTo(x + levelSize / 2, y + levelSize / 3, x + levelSize / 6, y + levelSize / 2, x, y + levelSize / 3); // Draw a bezier curve to the bottom middle point
      ctx.bezierCurveTo(x - levelSize / 6, y + levelSize / 2, x - levelSize / 2, y + levelSize / 3, x - levelSize / 2, y); // Draw a bezier curve to the left bottom point
      ctx.bezierCurveTo(x - levelSize / 2, y - levelSize / 3, x - levelSize / 6, y - levelSize / 2, x, y - levelSize / 6); // Draw a bezier curve to the top middle point
      ctx.closePath(); // Close the path
      break;
    case "diamond":
      ctx.moveTo(x, y - levelSize / 2); // Move to the top point
      ctx.lineTo(x + levelSize / 2, y); // Draw a line to the right point
      ctx.lineTo(x, y + levelSize / 2); // Draw a line to the bottom point
      ctx.lineTo(x - levelSize / 2, y); // Draw a line to the left point
      ctx.closePath(); // Close the path
      break;
    case "cross":
      ctx.moveTo(x - levelSize / 4, y - levelSize / 4); // Move to the top left inner point
      ctx.lineTo(x - levelSize / 4, y - levelSize / 2); // Draw a line to the top left outer point
      ctx.lineTo(x + levelSize / 4, y - levelSize / 2); // Draw a line to the top right outer point
      ctx.lineTo(x + levelSize / 4, y - levelSize / 4); // Draw a line to the top right inner point
      ctx.lineTo(x + levelSize / 2, y - levelSize / 4); // Draw a line to the right top outer point
      ctx.lineTo(x + levelSize / 2, y + levelSize / 4); // Draw a line to the right bottom outer point
      ctx.lineTo(x + levelSize / 4, y + levelSize / 4); // Draw a line to the right bottom inner point
      ctx.lineTo(x + levelSize / 4, y + levelSize / 2); // Draw a line to the bottom right outer point
      ctx.lineTo(x - levelSize /4, y + levelSize / 2); // Draw a line to the bottom left outer point
      ctx.lineTo(x - levelSize / 4, y + levelSize / 4); // Draw a line to the bottom left inner point
      ctx.lineTo(x - levelSize / 2, y + levelSize / 4); // Draw a line to the left bottom outer point
      ctx.lineTo(x - levelSize / 2, y - levelSize / 4); // Draw a line to the left top outer point
      ctx.closePath(); // Close the path
      break;
    case "moon":
      ctx.arc(x, y, levelSize / 2, Math.PI / 4, Math.PI * 5 / 4); // Draw a circle from the top right to the bottom left
      ctx.arc(x - levelSize / 6, y, levelSize / 3, Math.PI * 5 / 4, Math.PI / 4); // Draw a smaller circle from the bottom left to the top right
      ctx.closePath(); // Close the path
      break;
    default:
      break;
  }

  // Fill the icon shape with color
  ctx.fillStyle = levelColors[i];
  ctx.fill();

  // Draw the icon text using emoji
  ctx.font = "30px Arial";
  ctx.textAlign = "center";
  ctx.textBaseline = "middle";
  ctx.fillStyle = "white";
  ctx.fillText(levelIcons[i], x, y);
}

