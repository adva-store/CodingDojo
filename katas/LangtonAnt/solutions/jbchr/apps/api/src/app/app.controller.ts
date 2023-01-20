import { Direction } from '@langton-ant/shared-model';
import {
  Controller,
  Get,
  Query,
  StreamableFile,
  ValidationPipe,
} from '@nestjs/common';
import { LangtonAntService } from './langton-ant.service';

import { IsInt, IsString } from 'class-validator';
import { Readable } from 'stream';
import { stringify } from './utils/game-helper';

class GameQuery {
  @IsInt()
  boardLength: number;

  @IsInt()
  antPosX: number;

  @IsInt()
  antPosY: number;

  @IsString()
  antDirection: Direction;

  @IsInt()
  turns: number;
}

@Controller()
export class AppController {
  constructor(private readonly langtonAntService: LangtonAntService) {}

  @Get('game.csv')
  getFile(
    @Query(
      new ValidationPipe({
        transformOptions: { enableImplicitConversion: true },
        forbidNonWhitelisted: true,
      })
    )
    game: GameQuery
  ) {
    const result = this.langtonAntService.computeGame(
      game.boardLength,
      game.antPosX,
      game.antPosY,
      game.antDirection,
      game.turns
    );

    const resultString = stringify(result);

    return new StreamableFile(Readable.from(resultString));
  }
}
