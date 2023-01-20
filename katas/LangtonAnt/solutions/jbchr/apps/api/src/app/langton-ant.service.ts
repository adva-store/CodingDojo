import { Injectable } from '@nestjs/common';
import { Direction, Field, Game, State } from '@langton-ant/shared-model';
import {
  moveAnt,
  rotateAnt,
  switchState,
  removeAnt,
  addAnt,
} from './utils/game-helper';
import { coordsToPos } from './utils/grid-helper';

@Injectable()
export class LangtonAntService {
  /**
   * Computes a langton ant game.
   *
   * The algorithm operates on an array of turns. Each turn is represented as an one dimensional array that represents the grid.
   * Each field consists of the state as second char (s/w for black/white) and the ant's direction (n,o,s,w, space for north, east, south, west, no ant) as first char.
   * The algorithm's data structure is close to the outputs data structure, so in the end only the spaces are removed and the
   * fields are joined with a comma and the turns with a semilcolon.
   *
   * Note: The grid is infinite. If the ant moves out of boundary in the east direction it will return in the west
   *
   * @example
   * computeGame(2, 0, 0, 'n', 2)
   * Returns
   * nw,w,w,w;
   * s,ow,w,w
   *
   * @returns the game as string. Each turn is seperated with a semilkolon, each field in a turn is seperated by comma.
   * Each filed has the state (s/w for black/white) and optionaly as prefix the ant's direction (n,o,s,w for north, east, south, west)
   */
  computeGame(
    boardLength: number,
    antPosX: number,
    antPosY: number,
    antDirection: Direction,
    turns: number
  ): Game {
    // Build the game
    const game: Game = [Array(boardLength * boardLength).fill(' w')];
    let antPos = coordsToPos(antPosX, antPosY, boardLength);
    game[0][antPos] = `${antDirection}w`;

    let antState: State = 'w';
    let antDir = antDirection;

    for (let i = 1; i < turns; i++) {
      // Copy previous state
      game[i] = [...game[i - 1]];

      // Rotate ant
      antDir = rotateAnt(antDir, antState);

      // Switch fields & Remove ant
      game[i][antPos] = switchState(game[i][antPos]);
      game[i][antPos] = removeAnt(game[i][antPos]);

      // 3. Move ant
      antPos = moveAnt(antPos, antDir, boardLength);
      game[i][antPos] = addAnt(game[i][antPos], antDir);

      // 4. Get the new field's color
      antState = game[i][antPos][1] as State;
    }

    // Generate string output from game
    return game;
  }
}
