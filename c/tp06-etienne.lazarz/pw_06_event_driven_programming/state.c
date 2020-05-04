// Sets the 'Play' state.
void set_play(Game* game)
{
    // TODO:
    // - Set the state field to PLAY.
    // - Set the label of the start button to "Pause".
    // - Disable the stop button.
    // - Set the on_move_disc() function to be called at regular intervals.
}

// Sets the 'Pause' state.
void set_pause(Game* game)
{
    // TODO:
    // - Set the state field to PAUSE.
    // - Set the label of the start button to "Resume".
    // - Enable the stop button.
    // - Stop the on_move_disc() function.
}

// Sets the 'Stop' state.
void set_stop(Game *game)
{
    // TODO:
    // - Set the state field to STOP.
    // - Set the label of the start button to "Start".
    // - Disable the stop button.
}

// Event handler for the "clicked" signal of the start button.
void on_start(GtkButton *button, gpointer user_data)
{
    // Gets the `Game` structure.
    Game *game = user_data;

    // Sets the next state according to the current state.
    switch (game->state)
    {
        case STOP: set_play(game); break;
        case PLAY: set_pause(game); break;
        case PAUSE: set_play(game); break;
    };
}

// Event handler for the "clicked" signal of the stop button.
void on_stop(GtkButton *button, gpointer user_data)
{
    set_stop(user_data);
}

int main (int argc, char *argv[])
{
    // ... Snip ...

    g_signal_connect(stop_button, "clicked", G_CALLBACK(on_stop), &game);

    // ... Snip ...
}
