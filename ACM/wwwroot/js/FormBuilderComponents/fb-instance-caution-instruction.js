(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueFBInstanceCautionInstruction = factory());
}(this, (function () {
    var index = {
        name: 'fb-instance-caution-instruction',
        props: ['value'],
        data: function () {
            return {
                expanded: true
            };
        },
        methods: {
            toggleExpand: function () {
                this.expanded = !this.expanded;
            }
        },
        template: `<div>
                    <div class="row caution" v-if="value.definitionProperties.isCaution">
                        <div class="col-xs-12">
                            <div v-if="value.definitionProperties.cautionDescription != null && value.definitionProperties.cautionDescription != ''" v-bind:style="[{'background-color': value.definitionProperties.cautionColour}, {'float': 'left'}, {'height': '12px'}, {'width': '12px'}, {'border-radius': '3px'}, {'margin-top': '2px'}, {'margin-right': '8px'}, {'display': 'inline-block'}]"></div>
                            <div v-if="value.definitionProperties.cautionDescription != null && value.definitionProperties.cautionDescription != ''" style="display:inline-block;float:left;margin-right:8px">{{value.definitionProperties.cautionDescription}}</div>
                            <div v-if="value.definitionProperties.cautionDescription2 != null && value.definitionProperties.cautionDescription2 != ''" v-bind:style="[{'background-color': value.definitionProperties.cautionColour2}, {'float': 'left'}, {'height': '12px'}, {'width': '12px'}, {'border-radius': '3px'}, {'margin-top': '2px'}, {'margin-right': '8px'}, {'display': 'inline-block'}]"></div>
                            <div v-if="value.definitionProperties.cautionDescription2 != null && value.definitionProperties.cautionDescription2 != ''" style="display:inline-block;float:left;margin-right:8px">{{value.definitionProperties.cautionDescription2}}</div><br /><br />
                        </div>
                    </div>
                    <div class="row instructions" v-if="value.definitionProperties.isInstruction == true">
                        <div class="col-xs-12 pb-1" v-if="expanded == true">
                            <span class="instruction" :href="'#question_instruction_' + value.formDefinitionID" v-on:click="toggleExpand()"><i class="fa fa-eye-slash"></i> Instructions</span>
                            <div :id="'question_instruction_' + value.formDefinitionID">
                                <div v-html="value.definitionProperties.instructionText"></div>
                                <img :src="value.definitionProperties.instructionImageBase64" v-if="value.definitionProperties.instructionImageBase64 != ''" class="img-responsive" />
                            </div>
                        </div>
                        <div class="col-xs-12 pb-1" v-if="expanded == false">
                            <span class="instruction" :href="'#question_instruction_' + value.formDefinitionID" v-on:click="toggleExpand()"><i class="fa fa-eye"></i> Instructions</span>
                            <div :id="'question_instruction_' + value.formDefinitionID" style="display:none">
                                <div v-html="value.definitionProperties.instructionText"></div>
                                <img :src="value.definitionProperties.instructionImageBase64" v-if="value.definitionProperties.instructionImageBase64 != ''" class="img-responsive" />
                            </div>
                        </div>
                    </div>
                </div>`
    }
    return index;
})));