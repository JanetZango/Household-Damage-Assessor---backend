(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueDrawingPad = factory());
}(this, (function () {
	class Draw {
		constructor(canvasObj, cursorObj, app, parent) {
			this.app = app;
			this.parent = parent;
			this.cursor = cursorObj;
			this.c = canvasObj;
			this.ctx = this.c.getContext('2d');

			this.mouseDown = false;
			this.mouseX = 0;
			this.mouseY = 0;

			this.tempHistory = [];

			this.setSize();

			this.listen();

			this.redraw();
		}

		listen() {
			this.c.addEventListener('mousedown', (e) => {
				this.mouseDown = true;
				this.mouseX = e.offsetX;
				this.mouseY = e.offsetY;
				this.setDummyPoint();
			});

			this.c.addEventListener('mouseup', () => {
				if (this.mouseDown) {
					this.setDummyPoint();
				}
				this.mouseDown = false;
			});

			this.c.addEventListener('mouseleave', () => {
				if (this.mouseDown) {
					this.setDummyPoint();
				}
				this.mouseDown = false;
			});

			this.c.addEventListener('mousemove', (e) => {
				this.moveMouse(e);

				if (this.mouseDown) {
					this.mouseX = this.mouseX;
					this.mouseY = this.mouseY;

					if (!this.app.options.restrictX) {
						this.mouseX = e.offsetX;
					}

					if (!this.app.options.restrictY) {
						this.mouseY = e.offsetY;
					}

					var item = {
						isDummy: false,
						x: this.mouseX,
						y: this.mouseY,
						c: this.app.color,
						r: this.app.size
					};

					this.app.history.push(item);
					this.draw(item, this.app.history.length);
				}
			});

			window.addEventListener('resize', () => {
				this.setSize();
				this.redraw();
			});
		}

		setSize() {
			this.c.width = this.c.offsetWidth;
			this.c.height = this.c.offsetHeight;
		}

		moveMouse(e) {
			let x = e.offsetX;
			let y = e.offsetY;

			this.cursor.style.transform = `translate(${x - 10}px, ${y - 10}px)`;
		}

		getDummyItem() {
			var lastPoint = this.app.history[this.app.history.length - 1];

			return {
				isDummy: true,
				x: lastPoint.x,
				y: lastPoint.y,
				c: null,
				r: null
			};
		}

		setDummyPoint() {
			var item = this.getDummyItem();
			this.app.history.push(item);
			this.draw(item, this.app.history.length);
		}

		redraw() {
			this.ctx.clearRect(0, 0, this.c.width, this.c.height);
			this.drawBgDots();

			if (!this.app.history.length) {
				return true;
			}

			this.app.history.forEach((item, i) => {
				this.draw(item, i);
			});
		}

		drawBgDots() {
			var gridSize = 50;
			this.ctx.fillStyle = 'rgba(0, 0, 0, .2)';

			for (var i = 0; i * gridSize < this.c.width; i++) {
				for (var j = 0; j * gridSize < this.c.height; j++) {
					if (i > 0 && j > 0) {
						this.ctx.beginPath();
						this.ctx.rect(i * gridSize, j * gridSize, 2, 2);
						this.ctx.fill();
						this.ctx.closePath();
					}
				}
			}
		}

		draw(item, i) {
			this.ctx.lineCap = 'round';
			this.ctx.lineJoin = "round";

			var prevItem = this.app.history[i - 2];

			if (i < 2) {
				return false;
			}

			if (!item.isDummy && !this.app.history[i - 1].isDummy && !prevItem.isDummy) {
				this.ctx.strokeStyle = item.c;
				this.ctx.lineWidth = item.r;

				this.ctx.beginPath();
				this.ctx.moveTo(prevItem.x, prevItem.y);
				this.ctx.lineTo(item.x, item.y);
				this.ctx.stroke();
				this.ctx.closePath();
			} else if (!item.isDummy) {
				this.ctx.strokeStyle = item.c;
				this.ctx.lineWidth = item.r;

				this.ctx.beginPath();
				this.ctx.moveTo(item.x, item.y);
				this.ctx.lineTo(item.x, item.y);
				this.ctx.stroke();
				this.ctx.closePath();
			}
		}
	}

    var index = {
        name: 'drawing-pad',
        props: ['value'],
        data: function () {
            return {
                draw: null,
                history: [],
                color: '#000',
                popups: {
                    showColor: false,
                    showSize: false,
                    showSave: false,
                    showOptions: false
                },
                options: {
                    restrictY: false,
                    restrictX: false
                },
                size: 8,
				colors: [
					'#000',
                    '#d4f713',
                    '#13f7ab',
                    '#13f3f7',
                    '#13c5f7',
                    '#138cf7',
                    '#1353f7',
                    '#2d13f7',
                    '#7513f7',
                    '#a713f7',
                    '#d413f7',
                    '#f713e0',
                    '#f71397',
                    '#f7135b',
                    '#f71313',
                    '#f76213',
                    '#f79413',
                    '#f7e013'],
                sizes: [6, 8, 12, 18, 24, 32, 48],
                weights: [2, 4, 6]
            };
		},
		watch: {
			value: function (newItems) {
				if (this.value !== undefined && this.value !== null && this.value !== "" && this.value.serializedPoints !== "") {
					this.history = JSON.parse(this.value.serializedPoints);
					this.draw.redraw();
				}
			},
			size: function () {
				console.log(this.size)
			},
			color: function () {
				console.log(this.color)
			}
		},
		mounted: function () {
			var canvas = this.$el.querySelector(".canvas");
			var cursor = this.$el.querySelector(".cursor");
			var parent = this.$el.querySelector(".drawing-pad");
			this.draw = new Draw(canvas, cursor, this, parent);

			if (this.value.serializedPoints !== undefined && this.value.serializedPoints !== null && this.value.serializedPoints !== "") {
				this.history = JSON.parse(this.value.serializedPoints);
				this.draw.redraw();
			}
        },
		methods: {
			setColor: function (colorItem) {
				this.color = colorItem;
				this.popups.showColor = !this.popups.showColor;
			},
			setSize: function (sizeItem) {
				this.size = sizeItem;
				this.popups.showSize = !this.popups.showSize;
			},
            removeHistoryItem: function() {
                this.history.splice(this.history.length - 2, 1);
                this.draw.redraw();
            },
			removeAllHistory: function () {
                this.history = [];
                this.draw.redraw();
			},
			saveItem: function () {
				var self = this;
				var canvas = self.$el.querySelector(".canvas");
				let imageData = canvas.toDataURL();
				//this.$emit('newimage', { img: imageData, history: this.history });

				var valueObj = _.tap(_.cloneDeep(self.value), v => {
					_.set(v, "serializedPoints", JSON.stringify(self.history));
					_.set(v, "drawingBase64", imageData);
				});
				this.update(valueObj);
			},
			update: function (value) {
				this.$emit('input', value);
            },
        },
		template: `<div><div class="drawing-pad">
		<canvas class="canvas">
		</canvas>
		<div class="cursor"></div>
		<div class="controls">
			<div class="btn-row">
				<div class="history" title="history">
					{{ history.length }}
				</div>
			</div>
			<div class="btn-row">
				<button type="button"
								v-on:click="removeHistoryItem"
								v-bind:class="{ disabled: !history.length }" title="Undo">
					<i class="fa fa-reply"></i>
				</button>
				<button type="button"
								v-on:click="removeAllHistory"
								v-bind:class="{ disabled: !history.length }" title="Clear all">
					<i class="fa fa-trash-alt"></i>
				</button>
			</div>
			
			<div class="btn-row">
				<button type="button" title="Brush options"
								v-on:click="popups.showOptions = !popups.showOptions">
					<i class="fa fa-pen"></i>
				</button>
				
				<div class="popup" v-if="popups.showOptions">
					<div class="popup-title">
						Options
					</div>
					<button type="button" title="Restrict movement vertical"
									v-on:click="options.restrictY = !options.restrictY; options.restrictX = false"
									v-bind:class="{ active: options.restrictY }">
						<i class="fa fa-arrow-right"></i>
						Restrict vertical
					</button>
					<button type="button" title="Restrict movement horizontal"
									v-on:click="options.restrictX = !options.restrictX; options.restrictY = false"
									v-bind:class="{ active: options.restrictX }">
						<i class="fa fa-arrow-up"></i>
						Restrict horizontal
					</button>
				</div>
				
			</div>

			<div class="btn-row">
				<button type="button" title="Pick a brush size"
								v-on:click="popups.showSize = !popups.showSize"
								v-bind:class="{ active: popups.showSize }">
					<i class="fa fa-dot-circle"></i>
					<span class="size-icon">
						{{ size }}
					</span>
				</button>
				
				<div class="popup" v-if="popups.showSize">
					<div class="popup-title">
						Brush size
					</div>
					<label v-for="sizeItem in sizes" class="size-item">
						<input type="radio" name="size" v-model="size" v-bind:value="sizeItem">
						<span class="size"
									v-bind:style="{width: sizeItem + 'px', height: sizeItem + 'px'}"
									v-on:click="setSize(sizeItem)"></span>
					</label>
				</div>
			</div>
			
			<div class="btn-row">
				<button type="button" title="Pick a color"
								v-on:click="popups.showColor = !popups.showColor"
								v-bind:class="{ active: popups.showColor }">
					<i class="fa fa-palette"></i>
					<span class="color-icon"
								v-bind:style="{backgroundColor: color}">
					</span>
				</button>
				
				<div class="popup" v-if="popups.showColor">
					<div class="popup-title">
						Brush color
					</div>
					<label v-for="colorItem in colors" class="color-item">
						<input type="radio"
									 name="color"
									 v-model="color"
									 v-bind:value="colorItem">
						<span v-bind:class="'color color-' + colorItem"
									v-bind:style="{backgroundColor: colorItem}"
									v-on:click="SetColor(colorItem)"></span>
					</label>
				</div>
			</div>
			
			<div class="btn-row">
				<button type="button" class="save" title="Save"
								v-on:click="saveItem">
					<i class="fa fa-save"></i>
				</button>
				
			</div>
		</div>
	</div></div>`
    };
    return index;
})));