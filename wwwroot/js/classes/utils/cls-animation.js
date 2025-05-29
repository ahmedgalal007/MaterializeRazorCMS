// Get references to the DOM elements
const animatedDiv = document.getElementById('animatedDiv');
const container1 = document.getElementById('container1');
const container2 = document.getElementById('container2');
const moveButton = document.getElementById('moveButton');

// Keep track of the current parent container of the animatedDiv
let currentContainer = container1;

// Add an event listener to the button to trigger the animation
moveButton.addEventListener('click', () => {
  // Determine the target container based on the current container
  const targetContainer = (currentContainer === container1) ? container2 : container1;

  // 1. Get the current position of the animatedDiv relative to the viewport.
  //    This is crucial to maintain its visual position on the screen
  //    while its DOM parent is being changed.
  const rect = animatedDiv.getBoundingClientRect();
  const originalX = rect.left;
  const originalY = rect.top;

  // 2. Temporarily change the animatedDiv's position to 'fixed'.
  //    This takes it out of the normal document flow and allows us to
  //    position it precisely using screen coordinates (originalX, originalY).
  animatedDiv.style.position = 'fixed';
  animatedDiv.style.left = originalX + 'px';
  animatedDiv.style.top = originalY + 'px';

  // 3. Temporarily append the animatedDiv to the <body>.
  //    This is done to ensure its 'fixed' positioning is relative to the viewport,
  //    and it's not affected by its previous parent's layout.
  document.body.appendChild(animatedDiv);

  // 4. Calculate the target position for the animatedDiv within the target container.
  //    We want to center the div in the target container.
  const targetContainerRect = targetContainer.getBoundingClientRect();
  const targetX = targetContainerRect.left + (targetContainerRect.width / 2) - (animatedDiv.offsetWidth / 2);
  const targetY = targetContainerRect.top + (targetContainerRect.height / 2) - (animatedDiv.offsetHeight / 2);

  // 5. Trigger the CSS transition by changing the 'left' and 'top' properties.
  //    A small setTimeout is used to ensure the browser has time to register
  //    the initial 'fixed' position before the new target position is applied.
  //    Without this, the transition might not occur smoothly.
  setTimeout(() => {
    animatedDiv.style.left = targetX + 'px';
    animatedDiv.style.top = targetY + 'px';
  }, 10); // A minimal delay of 10 milliseconds

  // 6. Listen for the 'transitionend' event. This event fires when the CSS transition
  //    (defined by `transition: all 0.5s ease-in-out;` in CSS) completes.
  //    Using `{ once: true }` ensures the listener is automatically removed after it fires.
  animatedDiv.addEventListener('transitionend', function handler() {
    // Re-parent the animatedDiv to its new target container.
    targetContainer.appendChild(animatedDiv);

    // Reset the styles of the animatedDiv to its original state for positioning
    // within its new parent. This ensures it's centered correctly.
    animatedDiv.style.position = 'absolute'; // Back to absolute positioning
    animatedDiv.style.left = '50%'; // Center horizontally
    animatedDiv.style.top = '50%';  // Center vertically
    animatedDiv.style.transform = 'translate(-50%, -50%)'; // Fine-tune centering

    // Update the currentContainer reference for the next click.
    currentContainer = targetContainer;
  }, { once: true }); // The { once: true } option makes the listener fire only once
});

// Initial centering of the animatedDiv within its first container when the page loads.
// This uses the CSS centering trick.
animatedDiv.style.left = '50%';
animatedDiv.style.top = '50%';
animatedDiv.style.transform = 'translate(-50%, -50%)';
