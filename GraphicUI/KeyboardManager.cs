using Microsoft.Xna.Framework.Input;

public class KeyboardManager {
    private KeyboardState currentKeyState;
    private KeyboardState previousKeyState;
    public void Update() {
        previousKeyState = currentKeyState;
        currentKeyState = Keyboard.GetState();
    }
    public KeyboardState GetState() {
        return currentKeyState;
    }
    public bool IsKeyHeld(Keys key) {
        return currentKeyState.IsKeyDown(key);
    }
    public bool IsKeyPressed(Keys key) {
        return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
    }
    public bool IsKeyReleased(Keys key) {
        return !currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyDown(key);
    }
}
