INCLUDE globals.ink

{ pokemon_name == "": -> main | ->already_chose}

=== main ===
Hello I'm a test Dialogue! #audio: beep_1
And thank you to find me!
Now please choose Pokemon...
Wich pokemon do you choose?
    + [Charmander]
        -> chosen("Charmander")
    + [Bulbasaur]
        -> chosen("Bulbasaur")
    + [Squirtle]
        -> chosen("Squirtle")
        
=== chosen(pokemon)===
~ pokemon_name = pokemon
You chose {pokemon}!
->END

=== already_chose ===
You already chose {pokemon_name}
->END